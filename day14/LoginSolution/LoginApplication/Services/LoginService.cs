using LoginApplication.Interfaces;
using LoginApplication.Models;
using LoginApplication.Exceptions;

namespace LoginApplication.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        public bool ValidateLogin(string username, string password)
        {
            var user=_userRepository.GetUser(username);
            if (user == null) {
                throw new NoUserFoundException();
            }
            return user.Password == password;
        }

    }
}
