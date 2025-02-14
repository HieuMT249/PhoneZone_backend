namespace phonezone_backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
        public string Branch { get; set; }
        public string Image { get; set; }
        public string ProductDescription { get; set; }
        public string Stock { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public ICollection<WishListItem> WishListItems { get; set; }
        public Specification Specification { get; set; }
    }
}
