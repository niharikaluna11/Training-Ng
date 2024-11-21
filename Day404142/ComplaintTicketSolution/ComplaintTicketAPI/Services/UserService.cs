using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ComplaintTicketAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRepository<int, UserProfile> _profileRepo;
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
            IEmailSender emailSender
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

                var addedUser = await _userRepo.Add(user);
                if (addedUser != null)
                {
                    // Send email and create profile/organization asynchronously
                    try
                    {
                        string body = GenerateEmailBody(addedUser.Roles.ToString(),
                            registerUser.FName,
                            registerUser.LName,
                            registerUser.Username);
                        string email = registerUser.Email;
                        SendMail("Your Account Has Been Created", email, body);
                    }
                    catch (Exception emailEx)
                    {
                        return new ErrorResponseDTO
                        {
                            Success = false,
                            ErrorMessage = $"An unexpected error occurred during registration. Failed to send email: {emailEx.Message}",
                            ErrorCode = 500
                        };
                    }

                    try
                    {
                        await CreateProfileOrOrganizationAsync(addedUser, registerUser);
                    }
                    catch (Exception profileEx)
                    {
                        return new ErrorResponseDTO
                        {
                            Success = false,
                            ErrorMessage = $"An unexpected error occurred while creating profile: {profileEx.Message}",
                            ErrorCode = 500
                        };
                    }

                    return RegisterUser(registerUser.Username, registerUser.Role.ToString(), registerUser.Email, user.Id);
                }

                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "User could not be added due to validation errors or other issues.",
                    ErrorCode = 400 // Bad Request
                };
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library like Serilog, NLog, etc.)
                // Logger.LogError(ex, "Registration failed.");

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
                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to Get Users");
            }

        }
    }
}
