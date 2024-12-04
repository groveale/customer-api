using System;

namespace groveale.Models
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; } // Reference to Account model
        public string Territory { get; set; }
        public double Probability { get; set; }
        public string StageName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Owner { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime CloseDate { get; set; }
    }
}