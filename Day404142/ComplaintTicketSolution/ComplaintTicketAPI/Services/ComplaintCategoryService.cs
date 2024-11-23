using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ComplaintCategoryService : IComplaintCategoryService
{
    private readonly ComplaintTicketContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintCategoryService> _logger;

    public ComplaintCategoryService(ComplaintTicketContext context, IMapper mapper, ILogger<ComplaintCategoryService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ComplaintCategoryResponseDTO>> GetAllComplaintCategories(int pagenum,int pagesize)
    {
        try
        {
            var categories = await _context.ComplaintCategories.ToListAsync();
            pagenum = Math.Max(pagenum, 1);
            pagesize = Math.Max(pagesize, 5);

            int total = categories.Count();
            int pageTotal=(int)Math.Ceiling((double)total / pagesize);

            var returncategories = categories
                                    .Skip((pagenum - 1) * pagesize)
                                    .Take(pagesize)
                                    .ToList();
            


            return _mapper.Map<IEnumerable<ComplaintCategoryResponseDTO>>(returncategories);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching complaint categories.");
            throw new Exception("An error occurred while fetching complaint categories.", ex);
        }
    }

    public async Task<ComplaintCategoryResponseDTO> AddComplaintCategory(ComplaintCategoryDTO categoryDto)
    {
        try
        {
            var category = _mapper.Map<ComplaintCategory>(categoryDto);
            _context.ComplaintCategories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<ComplaintCategoryResponseDTO>(category);
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Error occurred while adding a new complaint category.");
            throw new Exception("An error occurred while adding a new complaint category.", dbEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while adding a new complaint category.");
            throw new Exception("An unexpected error occurred while adding a new complaint category.", ex);
        }
    }
}
