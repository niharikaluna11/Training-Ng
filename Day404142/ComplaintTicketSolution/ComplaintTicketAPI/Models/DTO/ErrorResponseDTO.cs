﻿

namespace ComplaintTicketAPI.Models.DTO
{
    public class ErrorResponseDTO
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
    }
}