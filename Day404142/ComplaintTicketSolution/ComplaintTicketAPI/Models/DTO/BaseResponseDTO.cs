namespace ComplaintTicketAPI.Models.DTO
{
    public class BaseResponseDTO
    {

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

    }
}
