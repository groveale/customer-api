using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Data
{
    public class TicketDb : DbContext
    {
        public TicketDb(DbContextOptions<TicketDb> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
    }
    public class OrderDb : DbContext
    {
        public OrderDb(DbContextOptions<OrderDb> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }

    public class OrderHeaderDb : DbContext
    {
        public OrderHeaderDb(DbContextOptions<OrderHeaderDb> options) : base(options) { }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
    }

    public class OpportunityDb : DbContext
    {
        public OpportunityDb(DbContextOptions<OpportunityDb> options) : base(options) { }

        public DbSet<Opportunity> Opportunities { get; set; }
    }

    public class CustomerDb : DbContext
    {
        public CustomerDb(DbContextOptions<CustomerDb> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}