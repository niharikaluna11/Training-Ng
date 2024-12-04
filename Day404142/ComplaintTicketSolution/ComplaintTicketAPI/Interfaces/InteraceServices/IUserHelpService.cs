using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IUserHelpService
    {
        Task<IEnumerable<UserHelp>> GetAllQueriesAsync();
        Task<UserHelp> GetQueryByEmailAsync(string email);
        Task<UserHelp> GetQueryByEmailifAsync(string email);
        Task AddQueryAsync(UserHelp userHelp);
        Task UpdateQueryAsync(string email, string adminResponse);
    }
}
