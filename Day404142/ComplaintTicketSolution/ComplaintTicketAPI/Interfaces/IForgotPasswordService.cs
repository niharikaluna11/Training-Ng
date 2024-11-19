namespace ComplaintTicketAPI.Interfaces
{
    public interface IForgotPasswordService
    {
        Task SendPasswordResetLink(string email);
        Task<bool> VerifyResetOtp(string otp); // Verify OTP instead of token
        Task ResetPassword(string UsernameorEmail,string otp, string newPassword, string ConfirmNewPassword); // Reset password using OTP
    }
}
