using System;

namespace groveale.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Owner { get; set; }
        public string? CustomerServiceManager { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContact { get; set; }
        public string? Severity { get; set; }
        public string? Status { get; set; }
        public DateTime DateOpened { get; set; }
        public int DaysOpen { get; set; }
        public string? AccountName { get; set; }
    }
}