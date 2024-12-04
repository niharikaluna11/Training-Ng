using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Interfaces.InterfaceRepositories
{
    public interface IUserHelpRepository
    {
        Task<IEnumerable<UserHelp>> GetAllQueriesAsync();
        Task<UserHelp> GetQueryByEmailAsync(string email);
        Task<UserHelp> GetQueryByEmailifAsync(string email);
        Task AddQueryAsync(UserHelp userHelp);
        Task UpdateQueryAsync(UserHelp userHelp);
    }
}
