using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

public class ComplaintCategoryService : IComplaintCategoryService
{
    private readonly ComplaintTicketContext _context;
    private readonly IMapper _mapper;
//asdkjf
    public ComplaintCategoryService(ComplaintTicketContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ComplaintCategoryResponseDTO>> GetAllComplaintCategories()
    {
        var categories = await _context.ComplaintCategories.ToListAsync();
        return _mapper.Map<IEnumerable<ComplaintCategoryResponseDTO>>(categories);
    }

    public async Task<ComplaintCategoryResponseDTO> AddComplaintCategory(ComplaintCategoryDTO categoryDto)
    {
        var category = _mapper.Map<ComplaintCategory>(categoryDto);
        _context.ComplaintCategories.Add(category);
        await _context.SaveChangesAsync();
        return _mapper.Map<ComplaintCategoryResponseDTO>(category);
    }
}
