using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;
using PizzaStoreAPI.Models.DTOs;
using PizzaStoreAPI.Repositories;

namespace PizzaStoreAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<int, Order> _orderRepository;
        private readonly IRepository<int, Pizza> _pizzaRepository;

        public OrderService(IRepository<int, Order> orderRepository, IRepository<int, Pizza> pizzaRepository)
        {
            _orderRepository = orderRepository;
            _pizzaRepository = pizzaRepository;
        }

        // Create a new order for the customer
        public async Task<PizzaOrderDTO> CreateOrder(PizzaOrderDTO pizzaOrderDTO, int customerId)
        {
            var pizzas = await _pizzaRepository.GetAll();

            // Calculate total amount and create order
            var totalAmount = pizzas.Where(p => pizzaOrderDTO.PizzaIds.Contains(p.Id))
                                    .Sum(p => p.Price);

            var order = new Order
            {
                CustomerId = customerId,
                TotalAmount = totalAmount,
                
                IsPaymnetSuccess = false,
                PaymentMethod = pizzaOrderDTO.PaymentMethod
            };

            var createdOrder = await _orderRepository.Add(order);
            return new PizzaOrderDTO
            {
                OrderNumber = createdOrder.OrderNumber,
                TotalAmount = createdOrder.TotalAmount,
                PaymentMethod = createdOrder.PaymentMethod
            };
        }

        // Get all orders for a specific customer
        public async Task<OrderDTO> GetAllOrder(int customerId)
        {
            var orders = await _orderRepository.GetAll();
            var customerOrders = orders.Where(o => o.CustomerId == customerId);

            return new OrderDTO
            {
                Orders = customerOrders.Select(o => new OrderDetailsDTO
                {
                    OrderId = o.OrderNumber,
                    TotalAmount = o.TotalAmount,
                    PaymentMethod = o.PaymentMethod,
                    IsPaymentSuccess = o.IsPaymnetSuccess
                }).ToList()
            };
        }
    }
}
