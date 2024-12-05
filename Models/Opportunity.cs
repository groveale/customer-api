using System;
namespace groveale.Models
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ParentAccountId { get; set; } // New property
        public string? ParentAccount { get; set; } // New property
        public string? Account { get; set; }
        public string? Territory { get; set; }
        public string? Region { get; set; }
        public string? ServiceLine { get; set; } // New property
        public double Probability { get; set; }
        public string? StageName { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? Owner { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime CloseDate { get; set; }
    }
}