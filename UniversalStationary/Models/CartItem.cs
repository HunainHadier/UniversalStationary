﻿namespace UniversalStationary.Models
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
