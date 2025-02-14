using DocumentFormat.OpenXml.Drawing.Charts;

namespace phonezone_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Address {get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
