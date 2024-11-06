﻿using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDTO> Authenticate(LoginRequestDTO loginUser);

        Task<LoginResponseDTO> Register(RegisterUserDto registerUser);
    }
}
