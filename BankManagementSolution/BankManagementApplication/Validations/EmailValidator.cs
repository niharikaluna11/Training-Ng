using BankManagementApplication.Interfaces;
using BankManagementApplication.Interfaces.Repositories;
using BankManagementApplication.Models.DTOs;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace BankManagementApplication.Validations
{
    public class EmailValidator : ICustomValidator
    {
        

        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private readonly ICustomerRepository _repository;



        public EmailValidator(ICustomerRepository custRepository)
        {
            _repository = custRepository ?? throw new ArgumentNullException(nameof(custRepository));
        }

        public async Task<BaseResponseDTO> ValidateAsync(object value)
        {
            var email = value?.ToString();

            if (string.IsNullOrEmpty(email))
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Message = "Email cannot be null or empty."
                };
            }

            if (!Regex.IsMatch(email, EmailPattern))
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Message = "Invalid email format."
                };
            }

            return new BaseResponseDTO
            {
                Success = true,
                Message = "Email validation passed."
            };
        }
    }
}
