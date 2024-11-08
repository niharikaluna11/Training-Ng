using ComplaintTicketAPI.EmailModel;

namespace ComplaintTicketAPI.EmailInterface
{
    public interface IEmailSender
    {
        void SendEmail(Message email);
    }
}
