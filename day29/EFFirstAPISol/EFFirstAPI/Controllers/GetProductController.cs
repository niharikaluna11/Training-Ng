using EFFirstAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProductController : ControllerBase
    {
        private IProductService _productService;

        public GetProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProduct();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getProductById")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            try
            {
                var product = await _productService.GetProduct(Id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
