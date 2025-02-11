namespace groveale.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Convenience | Store | SuperStore
        public int MonthlyDamage { get; set; }
        public int MonthlyTheft { get; set; }
        public int MonthlyExpired { get; set; }
    }
}