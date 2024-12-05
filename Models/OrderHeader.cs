
namespace groveale.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public int OpportunityID { get; set; }
        public string? SalesDocNumber { get; set; }
        public string? SalesDocType { get; set; }
        public string? SalesOrganisation { get; set; }
        public string? DistributionChannel { get; set; }
        public int ParentCustomerID { get; set; }
        public string? ParentCustomer { get; set; }
        public string? LocalCustomerID { get; set; }
        public string? LocalCustomer { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CreatedBy { get; set; }
    }
}