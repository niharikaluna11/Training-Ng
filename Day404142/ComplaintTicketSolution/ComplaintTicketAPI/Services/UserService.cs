﻿using ComplaintTicketAPI.Interfaces;
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
                        string body = $@"
                                <html>
                                <head>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            background-color: #f0f8ff;
                                            color: #333;
                                        }}
                                        .header {{
                                            background-color: #0073e6;
                                            color: white;
                                            padding: 10px;
                                            text-align: center;
                                            font-size: 24px;
                                        }}
                                        .greeting {{
                                            font-size: 18px;
                                            color: #333;
                                        }}
                                        .content {{
                                            margin-top: 20px;
                                            color: #333;
                                        }}
                                        .credentials {{
                                            margin-top: 10px;
                                            font-weight: bold;
                                        }}
                                        .footer {{
                                            margin-top: 20px;
                                            font-size: 14px;
                                            color: #555;
                                        }}
                                        .signature {{
                                            color: #0073e6;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='header'>
                                        <h1>Welcome to TicketSolve!</h1>
                                    </div>

                                    <p class='greeting'>Dear <strong>{addedUser.Roles.ToString()} {registerUser.FName} {registerUser.LName}</strong>,</p>
    
                                    <p class='content'>We are pleased to inform you that your account has been successfully created.</p>
                                    <p class='content'>Below are your login credentials for accessing our service:</p>
    
                                    <div class='credentials'>
                                        <p><strong>Username:</strong> {registerUser.Username}</p>
                                        <p><strong>Password:</strong> {registerUser.Password}</p>
                                    </div>

                                    <p class='content footer'>Should you have any questions or require assistance, please feel free to contact our support team.</p>
                                    <p class='footer'>Best regards,<br/><span class='signature'>TicketSolve Team</span></p>
                                </body>
                                </html>";

                        string email = registerUser.Email;
                        SendMail("Your Account Has Been Created", email, body);

                    }
                    catch { throw new Exception("Mail Cannot be send Becuase of Invalid Mail"); }
               

                    await CreateProfileOrOrganizationAsync(addedUser, registerUser);
                    return new LoginResponseDTO { Username = user.Username,
                        Id = user.Id,
                        Token = await _tokenService.GenerateToken(new UserTokenDTO
                        {
                            Username = user.Username,
                            Role = user.Roles.ToString()
                        })
                    };
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
