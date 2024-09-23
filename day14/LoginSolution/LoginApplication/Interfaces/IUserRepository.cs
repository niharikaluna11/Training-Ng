using LoginApplication.Models;

namespace LoginApplication.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string username);
    }
}
