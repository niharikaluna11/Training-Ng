namespace ComplaintTicketAPI.Models.DTO.ResponseDTO
{
    public class BaseResponseDTO
    {

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

    }
}
