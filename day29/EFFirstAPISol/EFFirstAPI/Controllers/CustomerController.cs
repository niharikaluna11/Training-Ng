using EFFirstAPI.Interfaces;
using EFFirstAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EFFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerBasicService;

        public CustomerController(ICustomerService cutomerBasicService)
        {
            _customerBasicService = cutomerBasicService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO customer)
        {
            try
            {
                var customerId = await _customerBasicService.CreateCustomer(customer);
                return Ok(customerId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
