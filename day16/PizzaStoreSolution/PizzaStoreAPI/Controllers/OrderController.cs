using Microsoft.AspNetCore.Mvc;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models.DTOs;
using System.Diagnostics;

namespace PizzaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(int customerId)
        {
            try
            {
                var order = await _orderService.GetAllOrder(customerId);
                return Ok(order);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateOrder(PizzaOrderDTO pizzaOrderDTO, int customerId)
        {
            try
            {
              var order = await _orderService.CreateOrder(pizzaOrderDTO, customerId);
                return Ok(order);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }
    }
    }
