namespace phonezone_backend.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public int UsageLimit { get; set; }
        public bool isActive { get; set; }

        public ICollection<OrderCoupon> OrderCoupons { get; set; }
    }
}
