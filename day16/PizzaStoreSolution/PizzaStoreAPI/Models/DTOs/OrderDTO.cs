namespace PizzaStoreAPI.Models.DTOs
{
    public class OrderDTO : IEquatable<OrderDTO>
    {
        public int OrderNumber { get; set; }

        //public List<PizzaOrderDTO> Orders { get; set; }
        public List<OrderDetailsDTO> Orders { get; set; } = new List<OrderDetailsDTO>();
        public Customer customer { get; set; }

        //public OrderDTO()
        //{
        //    Orders = new List<PizzaOrderDTO>();
        //}

        public bool Equals(OrderDTO? other)
        {
            if (other == null)
                return false;

            return this.OrderNumber == other.OrderNumber;
        }
    }
}
