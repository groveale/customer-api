namespace groveale.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string SKU { get; set; } // 5 digits
        public string Name { get; set; }
        public int Sold { get; set; } // in last 30 days
        public int InStock { get; set; }
        public int Stolen { get; set; } // in last 30 days
        public int Damaged { get; set; } // in last 30 days
        public int EoLDays { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public int StoreNumber { get; set; }
        public string Type { get; set; } // Fresh | Shelf
    }
}