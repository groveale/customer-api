using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using groveale.Data;
using groveale.Models;
using System;

namespace groveale.Data
{
    public static class DatabaseInitializer
    {
        public static void EnsureDatabasesCreated(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var ticketDbContext = services.GetRequiredService<TicketDb>();
                ticketDbContext.Database.EnsureCreated();
                SeedTickets(ticketDbContext);

                var orderDbContext = services.GetRequiredService<OrderDb>();
                orderDbContext.Database.EnsureCreated();
                SeedOrders(orderDbContext);

                var opportunityDbContext = services.GetRequiredService<OpportunityDb>();
                opportunityDbContext.Database.EnsureCreated();
                SeedOpportunities(opportunityDbContext);

                var accountDbContext = services.GetRequiredService<AccountDb>();
                accountDbContext.Database.EnsureCreated();
                SeedAccounts(accountDbContext);
            }
        }

        private static void SeedTickets(TicketDb context)
        {
            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(
                    new Ticket { Title = "Login Issue", Owner = "Alice", CustomerServiceManager = "Bob", CustomerName = "Charlie", CustomerContact = "charlie@example.com", Severity = "High", Status = "Open", DateOpened = DateTime.Now.AddDays(-3), DaysOpen = 3 },
                    new Ticket { Title = "Payment Failure", Owner = "Dave", CustomerServiceManager = "Eve", CustomerName = "Frank", CustomerContact = "frank@example.com", Severity = "Medium", Status = "In Progress", DateOpened = DateTime.Now.AddDays(-1), DaysOpen = 1 },
                    new Ticket { Title = "Account Locked", Owner = "Grace", CustomerServiceManager = "Heidi", CustomerName = "Ivan", CustomerContact = "ivan@example.com", Severity = "Low", Status = "Closed", DateOpened = DateTime.Now.AddDays(-7), DaysOpen = 7 }
                );
                context.SaveChanges();
            }
        }

        private static void SeedOrders(OrderDb context)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order { Title = "Order 1001", AccountName = "Acme Corp", Territory = "North America", Status = OrderStatus.Open, OrderValue = 15000, Currency = "USD", DateCreated = DateTime.Now.AddDays(-10), Products = new List<Product> { new Product { Name = "Widget A" }, new Product { Name = "Widget B" } } },
                    new Order { Title = "Order 1002", AccountName = "Globex Inc", Territory = "Europe", Status = OrderStatus.Shipped, OrderValue = 25000, Currency = "EUR", DateCreated = DateTime.Now.AddDays(-20), DateClosed = DateTime.Now.AddDays(-5), Products = new List<Product> { new Product { Name = "Gadget X" }, new Product { Name = "Gadget Y" } } },
                    new Order { Title = "Order 1003", AccountName = "Initech", Territory = "Asia", Status = OrderStatus.Closed, OrderValue = 5000, Currency = "JPY", DateCreated = DateTime.Now.AddDays(-30), DateClosed = DateTime.Now.AddDays(-15), Products = new List<Product> { new Product { Name = "Thingamajig" } } }
                );
                context.SaveChanges();
            }
        }

        private static void SeedOpportunities(OpportunityDb context)
        {
            if (!context.Opportunities.Any())
            {
                context.Opportunities.AddRange(
                    new Opportunity { Name = "Big Deal", Description = "Potential large contract with Acme Corp", AccountName = "Acme Corp", Territory = "North America", Probability = 0.9, StageName = "Negotiation", Amount = 100000, Currency = "USD", Owner = "Alice", DateCreated = DateTime.Now.AddDays(-60), CloseDate = DateTime.Now.AddDays(30) },
                    new Opportunity { Name = "Expansion Project", Description = "Expanding services to Globex Inc", AccountName = "Globex Inc", Territory = "Europe", Probability = 0.7, StageName = "Proposal", Amount = 75000, Currency = "EUR", Owner = "Bob", DateCreated = DateTime.Now.AddDays(-45), CloseDate = DateTime.Now.AddDays(15) },
                    new Opportunity { Name = "New Market Entry", Description = "Entering new market with Initech", AccountName =  "Initech", Territory = "Asia", Probability = 0.5, StageName = "Qualification", Amount = 50000, Currency = "JPY", Owner = "Charlie", DateCreated = DateTime.Now.AddDays(-30), CloseDate = DateTime.Now.AddDays(60) }
                );
                context.SaveChanges();
            }
        }

        private static void SeedAccounts(AccountDb context)
        {
            if (!context.Accounts.Any())
            {
                context.Accounts.AddRange(
                    new Account { Name = "Acme Corp", Strategic = true },
                    new Account { Name = "Globex Inc", Strategic = false },
                    new Account { Name = "Initech", Strategic = true }
                );
                context.SaveChanges();
            }
        }
    }
}