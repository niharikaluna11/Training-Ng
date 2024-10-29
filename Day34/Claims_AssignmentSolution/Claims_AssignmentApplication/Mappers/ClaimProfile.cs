using AutoMapper;
using Claims_AssignmentApplication.Models;
using Claims_AssignmentApplication.Models.DTOs;
using System.Security.Claims;

namespace ReportClaimApplication.Mappers
{
    internal class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claims, ClaimResponseDTO>().ReverseMap();   // Bidirectional mapping
            //CreateMap<Claims, ClaimSubmissionDTO>().ReverseMap();
            CreateMap<Claims, ClaimRequestDTO>().ReverseMap();

            // Mapping from Document to DocumentUploadDTO and vice versa
            CreateMap<Document, DocumentUploadDTO>().ReverseMap();
        }
    }
}