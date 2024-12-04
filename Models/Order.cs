using System;
using System.Collections.Generic;

namespace groveale.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Title { get; set; }
         public string? AccountName { get; set; } 
        public string? Territory { get; set; }
        public OrderStatus Status { get; set; }
        public decimal OrderValue { get; set; }
        public string? Currency { get; set; }
        //public List<Product> Products { get; set; } = new List<Product>();
        public List<string>? Products { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateClosed { get; set; }
    }

    public enum OrderStatus
    {
        Open,
        Shipped,
        Closed
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}