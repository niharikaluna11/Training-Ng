namespace EFFirstAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreationDate { get; set; }

        public bool? WishList { get; set; }
        
        // bool wishlist
        // datetime purchasedate
        public IEnumerable<CartItem> CartItems { get; set; }
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
