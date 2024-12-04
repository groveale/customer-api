using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Data
{
    public class OrderDb : DbContext
    {
        public OrderDb(DbContextOptions<OrderDb> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }

    public class TicketDb : DbContext
    {
        public TicketDb(DbContextOptions<TicketDb> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
    }

    public class OpportunityDb : DbContext
    {
        public OpportunityDb(DbContextOptions<OpportunityDb> options) : base(options) { }

        public DbSet<Opportunity> Opportunities { get; set; }
    }

    public class AccountDb : DbContext
    {
        public AccountDb(DbContextOptions<AccountDb> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
    }
}