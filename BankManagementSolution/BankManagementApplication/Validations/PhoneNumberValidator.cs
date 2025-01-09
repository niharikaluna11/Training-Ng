using BankManagementApplication.Interfaces;
using BankManagementApplication.Interfaces.Repositories;
using BankManagementApplication.Models.DTOs;
using System.Text.RegularExpressions;

namespace BankManagementApplication.Validations
{
    public class PhoneNumberValidator : ICustomValidator
    {
        private const string PhoneNumberPattern = @"^\d{10}$";
        private readonly ICustomerRepository _repository;

        public PhoneNumberValidator(ICustomerRepository custRepository)
        {
            _repository = custRepository ?? throw new ArgumentNullException(nameof(custRepository));
        }

        public async Task<BaseResponseDTO> ValidateAsync(object value)
        {
            var phoneNumber = value?.ToString();

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Message = "Phone number cannot be null or empty."
                };
            }

            if (!Regex.IsMatch(phoneNumber, PhoneNumberPattern))
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Message = "Phone number must contain exactly 10 digits."
                };
            }

            return new BaseResponseDTO
            {
                Success = true,
                Message = "Phone number validation passed."
            };
        }
    }
}
