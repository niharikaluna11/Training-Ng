namespace EFFirstAPI.Models
{
    public class CartItem
    {
        public int SNo { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public CartItem()
        {
            Cart = new Cart();
            Product = new Product();
        }
    }
}
