using System.Text.Json.Serialization;

namespace phonezone_backend.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }
        public Product Product { get; set; }

    }
}
