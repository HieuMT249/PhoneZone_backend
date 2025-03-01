﻿using System.Text.Json.Serialization;

namespace phonezone_backend.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
