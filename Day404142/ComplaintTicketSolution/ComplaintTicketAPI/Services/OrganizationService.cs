using AutoMapper;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComplaintTicketAPI.Context;

namespace ComplaintTicketAPI.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ComplaintTicketContext _context;
        private readonly IMapper _mapper;

        public OrganizationService(ComplaintTicketContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync()
        {
            var organizations = await _context.Organizations.ToListAsync();
            return _mapper.Map<IEnumerable<OrganizationDTO>>(organizations);
        }
    }
}
