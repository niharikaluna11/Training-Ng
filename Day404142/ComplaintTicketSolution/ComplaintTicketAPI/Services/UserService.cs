using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Exceptions;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models.DTO.ResponseDTO;
using ComplaintTicketAPI.Repositories;
using ComplaintTicketAPI.Validations;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ComplaintTicketAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRepository<int, UserProfile> _profileRepo;
        private readonly IUserOtpRepository _userotpRepo;
        private readonly IRepository<int, Organization> _organizationRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        private readonly ComplaintTicketContext _context;
        public UserService(
            IUserRepository userRepository,
            IRepository<int, UserProfile> profileRepo,
            IRepository<int, Organization> organizationRepo,
            ILogger<UserService> logger,
            ITokenService tokenService,
            ComplaintTicketContext context
            , IMapper mapper,
            IEmailSender emailSender,
            IUserOtpRepository userotpRepo
            )
        {
            _context = context;
            _userRepo = userRepository;
            _profileRepo = profileRepo;
            _organizationRepo = organizationRepo;
            _mapper = mapper;
            _logger = logger;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _userotpRepo = userotpRepo;
        }


        private void SendMail(string title, string email, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        email },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }

        // Reactivate a user (soft delete)
        public async Task<User> ReactivateUserAsync(string key)
        {
            var user = await _userRepo.GetByUsernameOrEmail(key);
            if (user == null || !user.IsDeleted)
            {
                throw new CouldNotDeleteException("User is not Deactivated. It is activated already.");
            }

            try
            {
                // Mark the user as reactivated (not deleted)
                user.IsDeleted = false;
                user.PStatus = PersonStatus.Activated; // Set status to Activated

                // Log the update before saving
                _logger.LogInformation($"Attempting to reactivate user: {user.Username} (ID: {user.Id})");

                // Explicitly update the user in the DbContext to ensure tracking
                _context.Users.Update(user);

                // Save changes to the database
                int affectedRows = await _context.SaveChangesAsync();

                // If no rows were affected, something went wrong with saving
                if (affectedRows == 0)
                {
                    throw new Exception("No rows were affected by the reactivation operation.");
                }

                // Log success
                _logger.LogInformation($"User {user.Username} (ID: {user.Id}) was successfully reactivated.");

                return user;  // Return the updated user
            }
            catch (DbUpdateException dbEx)
            {
                // Log database-related errors
                _logger.LogError(dbEx, "Database update failed during reactivation operation.");
                throw new Exception("Database update failed while reactivating the user.", dbEx);
            }
            catch (Exception ex)
            {
                // Log general errors
                _logger.LogError(ex, "An error occurred while reactivating the user.");
                throw new Exception("An error occurred while reactivating the user.", ex);
            }
        }


        public async Task<User> SoftDeleteUserAsync(string key)
        {
            var user = await _userRepo.GetByUsernameOrEmail(key);
            if (user == null)
            {
                throw new CouldNotDeleteException("User not found.");
            }
            if (user.IsDeleted==true)
            {
                throw new CouldNotDeleteException("User is already Deactivated");
            }

            try
            {
                // Mark the user as soft deleted
                user.IsDeleted = true;
                user.PStatus = PersonStatus.Deactivated;

                // Explicitly update the user in the DbContext to ensure tracking
                _context.Users.Update(user);

                // Save changes to the database
                await _context.SaveChangesAsync();

                return user;  // Return the updated user
            }
            catch (Exception ex)
            {
                // Log any errors and rethrow the exception
                _logger.LogError(ex, "An error occurred while deleting the user.");
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }



        public async Task<BaseResponseDTO> Authenticate(LoginRequestDTO loginUser)
        {
            try
            {
                // Fetch the user by username or email
                var user = await _userRepo.GetByUsernameOrEmail(loginUser.UsernameOrEmail);
                if (user == null)
                {
                    return new ErrorResponseDTO
                    {
                        Success = false,
                        ErrorMessage = " User not Found ",
                        ErrorCode = 401 // Example error code, replace with appropriate one
                    };
                }

                // Verify the password using the stored hash key
                using var hmac = new HMACSHA256(user.HashKey);
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password));
                if (!passwordHash.SequenceEqual(user.Password))
                {
                    return new ErrorResponseDTO
                    {
                        Success = false,
                        ErrorMessage = "The password is incorrect. Please try again",
                        ErrorCode = 401
                    };
                }



                var userDetails = new LoginResponseDTO
                {
                    Username = user.Username,
                    Email = user.Email,
                    Id = user.Id,
                    Role = user.Roles.ToString(),
                    Token = await _tokenService.GenerateToken(new UserTokenDTO
                    {
                        Username = user.Username,
                        Role = user.Roles.ToString()
                    })
                };

                // Return a success response with the DTO
                return new SuccessResponseDTO<LoginResponseDTO>
                {
                    Success = true,
                    Message = $"The User {loginUser.UsernameOrEmail} is successfully Login",
                    Data = userDetails
                };
            }

            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error during authentication for user: {Login}", user.UsernameOrEmail);

                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "An unexpected error occurred during authentication.",
                    ErrorCode = 500
                };
            }
        }


        public SuccessResponseDTO<LoginResponseDTO> RegisterUser(string username, string role, string email, int id)
        {
            // Create a DTO instance with user details
            var userDetails = new LoginResponseDTO
            {
                Username = username,
                Role = role,
                Email = email,
                Id = id
            };

            // Return a success response with the DTO
            return new SuccessResponseDTO<LoginResponseDTO>
            {
                Success = true,
                Message = $"The User  {userDetails.Username}  is successfully registered",
                Data = userDetails
            };
        }

        public async Task<BaseResponseDTO> Register(RegisterUserDto registerUser)
        {
            try
            {
                // Validate username explicitly using UsernameValidator
                var usernameValidator = new UsernameValidator(_userRepo);
                var usernameValidationResult = await usernameValidator.ValidateAsync(registerUser.Username);

                if (!usernameValidationResult.Success)
                {
                    return usernameValidationResult; // Return validation error directly
                }

                var emailValidator = new EmailValidator(_userRepo);
                var emailValidationResult = await emailValidator.ValidateAsync(registerUser.Email);

                if (!emailValidationResult.Success)
                {
                    return emailValidationResult; // Return validation error directly
                }
                var passwordValidator = new PasswordValidator(_userRepo);
                var passwordValidationResult = await passwordValidator.ValidateAsync(registerUser.Password, registerUser.Username);

                if (!passwordValidationResult.Success)
                {
                    return passwordValidationResult; // Return validation error directly
                }


                // Check OTP
                var otpResponse = await VerifyRegistrationOtp(registerUser.Email, registerUser.Otp);

                if (!otpResponse.Success)
                {
                    return otpResponse; // Return OTP validation error
                }

                
                using var hmac = new HMACSHA256();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password));

                var user = new User
                {
                    Username = registerUser.Username,
                    Email = registerUser.Email,
                    Password = passwordHash,
                    HashKey = hmac.Key,
                    Roles = registerUser.Role
                };

                // Add the user to the repository
                var addedUser = await _userRepo.Add(user);

                if (addedUser != null)
                {
                    // Attempt to send a confirmation email
                    try
                    {
                        string body = GenerateEmailBody(
                            addedUser.Roles.ToString(),
                            registerUser.FName,
                            registerUser.LName,
                            registerUser.Username
                        );
                        string email = registerUser.Email;
                        SendMail("Your Account Has Been Created", email, body);
                    }
                    catch (Exception emailEx)
                    {
                        return new ErrorResponseDTO
                        {
                            Success = false,
                            ErrorMessage = $"Registration successful, but email could not be sent: {emailEx.Message}",
                            ErrorCode = 500 // Internal Server Error
                        };
                    }

                    // Attempt to create a profile or organization
                    try
                    {
                        await CreateProfileOrOrganizationAsync(addedUser, registerUser);
                    }
                    catch (Exception profileEx)
                    {
                        return new ErrorResponseDTO
                        {
                            Success = false,
                            ErrorMessage = $"Registration successful, but profile creation failed: {profileEx.Message}",
                            ErrorCode = 500 // Internal Server Error
                        };
                    }

                    // Return success response
                    return RegisterUser(
                        registerUser.Username,
                        registerUser.Role.ToString(),
                        registerUser.Email,
                        addedUser.Id
                    );
                }

                // If the user was not added successfully, return an error
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "User could not be added due to validation errors or other issues.",
                    ErrorCode = 400 // Bad Request
                };
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = $"Registration failed due to an unexpected error: {ex.Message}",
                    ErrorCode = 500 // Internal Server Error
                };
            }
        }




        private string GenerateEmailBody(string role, string firstName, string lastName, string username)
        {
            return $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #ffffff;
                color: #000000;
                margin: 0;
                padding: 20px;
                line-height: 1.6;
            }}
            .header {{
                text-align: center;
                font-size: 24px;
                font-weight: bold;
                margin-bottom: 20px;
            }}
            .content {{
                margin-top: 20px;
            }}
            .credentials {{
                margin-top: 10px;
                font-weight: bold;
            }}
            .footer {{
                margin-top: 30px;
                font-size: 14px;
                color: #333333;
            }}
            .signature {{
                font-weight: bold;
            }}
        </style>
        </head>
        <body>
            <div class='header'>
                Welcome to TicketSolve
            </div>

            <p>Dear <strong>{firstName} {lastName}</strong>,</p>

            <p>We are pleased to inform you that your account has been successfully created.</p>
            <p>Below are your login credentials for accessing our service:</p>

            <div class='credentials'>
                <p><strong>Username:</strong> {username}</p>
                <p><strong>Role:</strong> {role}</p>    
            </div>

            <p class='content'>Should you have any questions or require assistance, please feel free to contact our support team.</p>

            <p class='footer'>
                Best regards,<br/>
                <span class='signature'>TicketSolve Team</span>
            </p>
        </body>
        </html>";
        }



        private async Task CreateProfileOrOrganizationAsync(User addedUser, RegisterUserDto registerUser)
        {
            try
            {
                if (addedUser.Roles == Role.Organization)
                {
                    await CreateOrganizationProfileAsync(addedUser.Id, registerUser);
                }
                else
                {
                    await CreateUserProfileAsync(addedUser.Id, registerUser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating profile for user: {UserId}", addedUser.Id);
                throw new Exception("Failed to create profile or organization");
            }
        }

        private async Task CreateUserProfileAsync(int userId, RegisterUserDto registerUser)
        {
            try
            {
                var profile = new UserProfile
                {
                    UserId = userId,
                    FirstName = registerUser.FName,
                    LastName = registerUser.LName,
                    Address = string.Empty,
                    DateOfBirth = registerUser.DateOfBirth,
                    Email = registerUser.Email,
                    Phone = string.Empty,
                    Preferences = string.Empty,
                };
                await _profileRepo.Add(profile);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user profile for user: {UserId}", userId);
                throw new Exception("Failed to create user profile");
            }
        }

        private async Task CreateOrganizationProfileAsync(int userId, RegisterUserDto registerUser)
        {
            try
            {
                var organization = new Organization
                {
                    UserId = userId,
                    Name = registerUser.FName,
                    Email = registerUser.Email,
                    Phone = string.Empty,
                    Address = string.Empty,
                    Types = registerUser.Types ?? Models.Type.Company

                };
                await _organizationRepo.Add(organization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating organization profile for organization: {UserId}", userId);
                throw new Exception("Failed to create organization profile");
            }
        }

    
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                // No need to check for null as ToListAsync() will return an empty list if no users
                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                throw new Exception("Failed to Get Users", ex);
            }
        }


        public async Task<BaseResponseDTO> SendRegistrationOtp(string email)
        {
            var emailValidator = new EmailValidator(_userRepo);
            var emailValidationResult = await emailValidator.ValidateAsync(email);

            if (!emailValidationResult.Success)
            {
                return emailValidationResult; // Return validation error directly
            }

            var user = await _userRepo.GetByUsernameOrEmail(email);

            if (user != null)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "User already exists with this email.",
                    ErrorCode = 400
                };
            }

            // Generate a 6-digit OTP
            string otp = GenerateOtp();

            // Save the OTP and expiry time in a temporary user record or in-memory cache
            var tempUser = new UserOtp
            {
                Email = email,
                Otp = otp,
                OtpExpiry = DateTime.Now.AddMinutes(10)
            };

            await _userotpRepo.Add(tempUser);

            SendMail("Registration OTP", email, $"Your OTP for registration is: {otp} . Valid for 10 mins only");

            return new BaseResponseDTO
            {
                Success = true,
                Message = "OTP has been sent to your email."
            };
        }

        public async Task<BaseResponseDTO> VerifyRegistrationOtp(string email, string otp)
        {
            var emailValidator = new EmailValidator(_userRepo);
            var emailValidationResult = await emailValidator.ValidateAsync(email);

            if (!emailValidationResult.Success)
            {
                return emailValidationResult; // Return validation error directly
            }

            var user = await _userotpRepo.Get(email,otp);

            if (user == null || user.Otp != otp)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Invalid OTP or email.",
                    ErrorCode = 400
                };
            }

            if (user.OtpExpiry < DateTime.Now)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "OTP has expired.",
                    ErrorCode = 400
                };
            }

            return new BaseResponseDTO
            {
                Success = true,
                Message = "OTP is valid."
            };
        }


        private string GenerateOtp()
        {
            var random = new Random();
            string otp = random.Next(100000, 999999).ToString(); // Generates a 6-digit OTP
            return otp;
        }

    }
}
