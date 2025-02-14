namespace phonezone_backend.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<WishListItem> WishListItems { get; set; }
    }
}
