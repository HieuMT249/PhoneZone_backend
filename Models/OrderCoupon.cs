namespace phonezone_backend.Models
{
    public class OrderCoupon
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CouponId { get; set; }
        public string DiscountAmount { get; set; }

        public Order Order { get; set; }
        public Coupon Coupon { get; set; }
    }
}
