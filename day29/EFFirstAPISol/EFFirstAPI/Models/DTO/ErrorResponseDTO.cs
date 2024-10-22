namespace EFFirstAPI.Models.DTO
{
    public class ErrorResponseDTO
    {
        public int ErrorNumber { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }

}
