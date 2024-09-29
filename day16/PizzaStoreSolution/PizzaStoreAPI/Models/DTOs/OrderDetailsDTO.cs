namespace PizzaStoreAPI.Models.DTOs
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string OrderStatus { get; set; }
        public bool IsPaymentSuccess { get; set; }
        public List<Pizza> Pizzas { get; set; }
    }
}
