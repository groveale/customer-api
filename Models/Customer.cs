namespace groveale.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Strategic { get; set; }
        public string? AccountOwner { get; set; }
        public int? SellerID { get; set; }
        
    }
}