

using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Exceptions;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

// all done 

namespace ComplaintTicketAPI.Repositories
{
    public class UserOtpRepositoy : IUserOtpRepository
    {
        private readonly ComplaintTicketContext _context;

        public UserOtpRepositoy(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<UserOtp> Add(UserOtp entity)
        {
            try
            {
                await _context.UserOtp.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the OTP record.", ex);
            }
        }

        public Task<UserOtp> Delete(string key)
        {
            throw new NotImplementedException();
        }

       
        public async Task<UserOtp> Get(string key)
        {
            try
            {
                return await _context.UserOtp.FirstOrDefaultAsync(u => u.Email == key);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the OTP record.", ex);
            }
        }

        public async Task<UserOtp> Get(string email, string otp)
        {
            try
            {
                return await _context.UserOtp
                    .FirstOrDefaultAsync(u => u.Email == email && u.Otp == otp && u.OtpExpiry > DateTime.Now);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the OTP record.", ex);
            }
        }


        public Task<IEnumerable<UserOtp>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserOtp> Update(UserOtp entity, string key)
        {
            throw new NotImplementedException();
        }
    }
}
