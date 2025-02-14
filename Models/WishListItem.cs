namespace phonezone_backend.Models
{
    public class WishListItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WishListId { get; set; }

        public Product Product { get; set; }
        public WishList WishList { get; set; }

    }
}
