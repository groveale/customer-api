namespace groveale.Models
{

public class Invoice
{
    public int Id { get; set; }
    public string? SalesDocNumber { get; set; }
    public string? InvoiceNumber { get; set; }
    public int ParentCustomerID { get; set; }
    public string? ParentCustomer { get; set; }
    public string? LocalCustomerID { get; set; }
    public string? LocalCustomer { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? Currency { get; set; }
    public decimal TotalAmount { get; set; }
}
}