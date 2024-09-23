using LoginApplication.Interfaces;
using LoginApplication.Models;

namespace LoginApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users ;
        public UserRepository() {
            _users = new List<User>()
            {
                new User{Username="ng",Password="1102"},
                new User{Username="gg",Password="1102"}
            };

        }

        public User GetUser(string username)
        {
            return _users.FirstOrDefault(u=>u.Username==username);
        }

    }
}
