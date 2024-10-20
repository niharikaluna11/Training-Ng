namespace EFFirstAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string BasicImage { get; set; } = string.Empty;

        public List<ProductImage> ProductImages { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
           ProductImages = new List<ProductImage>();
            OrderDetails = new List<OrderDetail>();
        }
    }
}
