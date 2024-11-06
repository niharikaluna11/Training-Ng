using ComplaintTicketAPI.EmailConfig;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message email);
    }
}
