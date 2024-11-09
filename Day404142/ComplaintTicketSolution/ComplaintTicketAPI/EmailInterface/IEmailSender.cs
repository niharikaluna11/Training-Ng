using ComplaintTicketAPI.EmailModel;
using MimeKit;

namespace ComplaintTicketAPI.EmailInterface
{
    public interface IEmailSender
    {
        void SendEmail(Message email);
     
    }
}
