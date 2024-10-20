using EFFirstAPI.Interfaces;
using EFFirstAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EFFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        // private readonly ILogger _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            try
            {
                var productId = await _productService.CreateProduct(product);
                _logger.LogInformation("Product Added");
                return Ok(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("UpdateProductPrice")]

        public async Task<IActionResult> UpdateProductPrice(float price, int productId)
        {
            try
            {
                var product = await _productService.UpdateProductPrice(price, productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
