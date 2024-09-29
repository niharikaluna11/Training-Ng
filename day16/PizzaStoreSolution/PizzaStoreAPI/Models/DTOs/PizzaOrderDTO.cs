namespace PizzaStoreAPI.Models.DTOs
{
    public class PizzaOrderDTO : IEquatable<PizzaOrderDTO>
    {
        public int OrderNumber { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public List<int> PizzaIds { get; set; } // Add this for storing pizza IDs
        public OrderStatus OrderStatus { get; set; } // Change to enum

        bool IEquatable<PizzaOrderDTO>.Equals(PizzaOrderDTO? other)
        {
            return this.OrderNumber == other.OrderNumber;
        }
    }
}
