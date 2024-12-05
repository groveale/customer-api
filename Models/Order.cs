using System;
using System.Collections.Generic;

namespace groveale.Models
{
    public class Order
    {
        
        public int Id { get; set; }
        public int ParentCustomerID { get; set; }
        public string? ParentCustomer { get; set; }
        public string? LocalCustomer { get; set; }
        public string? LocalCustomerID { get; set; }
        public string? SalesOrderID { get; set; }
        public int UnitQuantity { get; set; }
        public decimal ItemValue { get; set; }
        public string? StorageLocation { get; set; }
        public string? OrderItemName { get; set; }
        public string? OrderItemDescription { get; set; }
        public string? ShippingDestinationCountry { get; set; }
        public string? ShippingDestinationCity { get; set; }
        public string? ProfitCentre { get; set; }
    }
}