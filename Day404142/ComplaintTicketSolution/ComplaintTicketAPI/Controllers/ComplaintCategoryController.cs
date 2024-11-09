using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ComplaintCategoryController : ControllerBase
{
    private readonly IComplaintCategoryService _service;

    public ComplaintCategoryController(IComplaintCategoryService service)
    {
        _service = service;
    }

    [HttpGet("GetAllCategories")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ComplaintCategoryResponseDTO>>> GetCategories()
    {
        var categories = await _service.GetAllComplaintCategories();
        return Ok(categories);
    }

    [HttpPost("CreateCategory")]
    [Authorize(Roles = "Admin,Organization")]
    public async Task<ActionResult<ComplaintCategoryResponseDTO>> CreateCategory(ComplaintCategoryDTO categoryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdCategory = await _service.AddComplaintCategory(categoryDto);
        return CreatedAtAction(nameof(GetCategories), new { id = createdCategory.Id }, createdCategory);
    }
}
