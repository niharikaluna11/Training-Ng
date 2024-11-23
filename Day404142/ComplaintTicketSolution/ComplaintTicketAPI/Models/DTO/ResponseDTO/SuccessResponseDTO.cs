namespace ComplaintTicketAPI.Models.DTO.ResponseDTO
{
    public class SuccessResponseDTO<T> : BaseResponseDTO
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

}
