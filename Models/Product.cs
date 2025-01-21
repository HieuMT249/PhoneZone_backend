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

        public virtual ProductDetail Details { get; set; }
    }
}
