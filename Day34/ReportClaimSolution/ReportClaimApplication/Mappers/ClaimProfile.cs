using AutoMapper;
using ReportClaimApplication.Models;
using ReportClaimApplication.Models.DTO;
using System.Security.Claims;

namespace ReportClaimApplication.Mappers
{
    internal class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            // Mapping from Claim to ClaimResponseDTO
            CreateMap<Claims, ClaimResponseDTO>()
                .ForMember(dest => dest.PolicyNumber, opt => opt.MapFrom(src => src.PolicyId.ToString())) // Assuming PolicyId is the policy number
                .ForMember(dest => dest.DocumentUrls, opt => opt.MapFrom(src => src.Documents.Select(doc => doc.DocumentUrl).ToList()));


            // Mapping from ClaimRequestDTO to Claim
            CreateMap<ClaimRequestDTO, Claims>()
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src =>
                    new List<Document> 
                    {
                        new Document
                        {
                            DocumentName = "Default Document",
                            DocumentUrl = "Default URL",       
                            UploadedDate = DateTime.Now       
                        }
                    }));

            // Mapping from Document to DocumentUploadDTO
            CreateMap<Document, DocumentUploadDTO>();
        }
    }
}