namespace BankManagementApplication.Models.DTOs
{
    public class ErrorResponseDTO : BaseResponseDTO
    {
        public string ErrorMessage { get; set; } 
        public int ErrorCode { get; set; }


    }
}
