using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ComplaintTicketAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _secretKey = configuration["JWT:SecretKey"];
            _logger = logger;
        }

        public virtual async Task<string> GenerateToken(UserTokenDTO user)
        {
            try
            {
                string _token = string.Empty;

                var _claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserID", user.userid.ToString()),
                    new Claim("Username", user.Username),
                };
                // Create claims based on user info
                //var _claims = new[]
                //{
                //    new Claim("UserId",user.userid),
                //    new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
                //    new Claim(ClaimTypes.Role, user.Role),
                    
                //};

                // Create security key from secret key
                var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

                // Set signing credentials
                var _credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

                // Create token descriptor
                var _tokenDescriptor = new JwtSecurityToken(
                    claims: _claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: _credentials
                );

                // Generate token
                _token = new JwtSecurityTokenHandler().WriteToken(_tokenDescriptor);
                return _token;
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Invalid argument while generating token.");
                throw new Exception("Error generating token due to invalid arguments.", argEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the JWT token.");
                throw new Exception("An unexpected error occurred while generating the token.", ex);
            }
        }
    }
}
