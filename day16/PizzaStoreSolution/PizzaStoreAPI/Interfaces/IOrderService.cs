using PizzaStoreAPI.Models.DTOs;

namespace PizzaStoreAPI.Interfaces
{
    public interface IOrderService
    {
        public Task<PizzaOrderDTO> CreateOrder(PizzaOrderDTO pizzaOrderDTO, int customerId);
        public Task<OrderDTO> GetAllOrder(int customerId);
    }
}
