using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;

namespace ComplaintTicketAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<string, User> _userRepo;
        private readonly IRepository<int, UserProfile> _profileRepo;
        private readonly IRepository<int, Organization> _organizationRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        private readonly ComplaintTicketContext _context;
        public UserService(
            IRepository<string, User> userRepository,
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


        private void SendMail(string title,string email, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        email },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }

        public async Task<LoginResponseDTO> Authenticate(LoginRequestDTO loginUser)
        {
            try
            {
                var user = await _userRepo.Get(loginUser.Username);
                if (user == null) throw new Exception("User not found");

                using var hmac = new HMACSHA256(user.HashKey);
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password));
                if (!passwordHash.SequenceEqual(user.Password))
                    throw new Exception("Invalid username or password");

                return new LoginResponseDTO
                {
                    Username = user.Username,
                    Id = user.Id,
                    Token = await _tokenService.GenerateToken(new UserTokenDTO
                    {
                        Username = user.Username,
                        Role = user.Roles.ToString()
                    })
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during authentication for user: {Username}", loginUser.Username);
                throw new Exception("Authentication failed");
            }
        }

        public async Task<LoginResponseDTO> Register(RegisterUserDto registerUser)
        {
            using var hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password));

            var user = new User
            {
                Username = registerUser.Username,
                Password = passwordHash,
                HashKey = hmac.Key,
                Roles = registerUser.Role
            };

            try
            {
                var addedUser = await _userRepo.Add(user);
                if (addedUser != null)
                {

                    try
                    {
                        string body = $"Dear {addedUser.Roles.ToString()} {registerUser.Name},\n\n" +
                                         "We are pleased to inform you that your account has been successfully created.\n\n" +
                                         "Below are your login credentials for accessing our service:\n\n" +
                                         $"*Username:* {registerUser.Username}\n" +
                                         $"*Password:* {registerUser.Password}\n\n" +
                                         "Should you have any questions or require assistance, please feel free to contact our support team.\n\n" +
                                         "Best regards,\n" +
                                         "ComplaintTicketApp\n" +
                                         "Support Team" +
                                         "(Niharika Garg)";

                        string email = registerUser.Email;
                        SendMail("Your Account Has Been Created", email, body);

                    }
                    catch { throw new Exception("Mail Cannot be send Becuase of Invalid Mail"); }
               

                    await CreateProfileOrOrganizationAsync(addedUser, registerUser);
                    return new LoginResponseDTO { Username = user.Username };
                }
                throw new Exception("User could not be added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration for user: {Username}", registerUser.Username);
                throw new Exception("Registration failed");
            }
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
                    FirstName = registerUser.Name,
                    LastName = string.Empty,
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
                    Name = registerUser.Name,
                    Email = registerUser.Email,
                    Phone = string.Empty,
                    Address = string.Empty,
                    Types = registerUser.Types
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
            catch(Exception ex)
            {
                throw new Exception("Failed to Get Users");
            }
            
        }
    }
}
