
using System.Runtime.CompilerServices;

namespace phonezone_backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string FinalAmount { get; set; }

        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string Color { get; set; }

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<OrderCoupon> OrderCoupons { get; set; }
    }
}
