using ComplaintTicketAPI.Models.DTO.ResponseDTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IForgotPasswordService
    {
        Task<BaseResponseDTO> SendPasswordResetLink(string email);
        Task<BaseResponseDTO> VerifyResetOtp(string otp); // Verify OTP instead of token
        Task<BaseResponseDTO> ResetPassword(string UsernameorEmail, string otp, string newPassword, string ConfirmNewPassword); // Reset password using OTP
    }
}
