using System;

namespace groveale.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Long_Description { get; set; } // New property
        public string? Priority { get; set; } // New property
        public string? CallerID { get; set; } // New property
        public string? State { get; set; } // New property
        public string? AssignedTo { get; set; } // New property
        public DateTime Opened_at { get; set; } // New property
        public DateTime? Closed_at { get; set; } // New property
        public int CompanyID { get; set; } // New property
        public int DaysOpen { get; set; }
    }
}