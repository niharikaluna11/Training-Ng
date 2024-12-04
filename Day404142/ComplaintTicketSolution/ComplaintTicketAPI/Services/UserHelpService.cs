using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Interfaces.InterfaceRepositories;
using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Services
{
    public class UserHelpService : IUserHelpService
    {

        private readonly IUserHelpRepository _repository;
        private readonly IEmailSender _emailSender;
        public UserHelpService(IUserHelpRepository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        private void SendMail(string title, string email, string body)
        {
            var message = new Message(new string[] { email }, title, body);
            _emailSender.SendEmail(message);

        }
        // Get all user queries
        public async Task<IEnumerable<UserHelp>> GetAllQueriesAsync()
        {
            return await _repository.GetAllQueriesAsync();
        }

        // Get a specific query by ID
        public async Task<UserHelp> GetQueryByEmailAsync(string email)
        {
            return await _repository.GetQueryByEmailAsync(email);
        }
        public async Task<UserHelp> GetQueryByEmailifAsync(string email)
        {
            return await _repository.GetQueryByEmailifAsync(email);
        }

        // Add a new user query
        public async Task AddQueryAsync(UserHelp userHelp)
        {
            // Check if there is already an existing query with the same email that has not been responded to
            var existingQuery = await _repository.GetQueryByEmailAsync(userHelp.email);

            if (existingQuery != null && !existingQuery.IsResponded)
            {
                throw new Exception("Please wait for the first reply before submitting another query.");
            }

            // If no query exists or the previous query has been responded to, add the new query
            await _repository.AddQueryAsync(userHelp);
        }

        // Update a query with the admin response
        public async Task UpdateQueryAsync(string email, string adminResponse)
        {
            var query = await _repository.GetQueryByEmailifAsync(email);
            if (query == null)
            {
                throw new Exception("Query not found.");
            }

            if (query.IsResponded)
            {
                throw new Exception("This query has already been responded to.");
            }

            query.AdminResponse = adminResponse;
            query.IsResponded = true;

            await _repository.UpdateQueryAsync(query);

            SendMail("Replying to your query", query.email, adminResponse);
        }
    }
}
