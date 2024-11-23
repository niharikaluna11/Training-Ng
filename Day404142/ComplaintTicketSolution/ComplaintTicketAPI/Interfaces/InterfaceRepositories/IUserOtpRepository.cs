using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Interfaces.InterfaceRepository
{
    public interface IUserOtpRepository : IRepository<string, UserOtp>
    {
        Task<UserOtp> Get(string email, string otp);
    }
}
