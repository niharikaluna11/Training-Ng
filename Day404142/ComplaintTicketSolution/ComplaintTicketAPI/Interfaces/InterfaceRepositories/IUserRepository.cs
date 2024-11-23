using System.Threading.Tasks;
using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Interfaces.InterfaceRepository
{
    public interface IUserRepository : IRepository<string, User>
    {
        Task<User> GetByUsernameOrEmail(string UsernameorEmail);
        Task<User?> GetUserByResetToken(string token);
        Task UpdateUser(User user);
        Task<bool> UserExists(string username);

        Task<bool> EmailExists(string email);

        Task<User> Reactivate(string key);

    }
}
