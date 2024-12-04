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

                var customerDbContext = services.GetRequiredService<CustomerDb>();
                customerDbContext.Database.EnsureCreated();
                SeedCustomers(customerDbContext);
            }
        }

        private static void SeedTickets(TicketDb context)
        {
             if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(
                    new Ticket { Title = "I can't access my account and need a password reset ASAP! This is affecting my work.", Priority = "High", CallerID = "allen.trevino@farbrikam.com", State = "New", AssignedTo = "Fatoumata.Diallo@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 1, DaysOpen = 1 },
                    new Ticket { Title = "VPN is down, and I can't work remotely. Fix this now! This is unacceptable.", Priority = "Critical", CallerID = "brenda.sanchez@wingtipstoys.com", State = "InProgress", AssignedTo = "Mei.Ling@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 1), CompanyID = 2, DaysOpen = 0 },
                    new Ticket { Title = "The office printer is not working again. Please fix it as soon as possible.", Priority = "Medium", CallerID = "patricia.lopez@wingtipstoys.com", State = "Resolved", AssignedTo = "Hunter.Stephens@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 3), CompanyID = 2, DaysOpen = 2 },
                    new Ticket { Title = "I need new software installed on my computer. It's for a project due next week.", Priority = "Low", CallerID = "kimberly.medina@farbrikam.com", State = "New", AssignedTo = "Sharon.Compton@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 5), CompanyID = 1, DaysOpen = 4 },
                    new Ticket { Title = "My email isn't syncing on my phone. This is urgent as I need to stay connected.", Priority = "High", CallerID = "dorothy.bennett@adventureworks.com", State = "New", AssignedTo = "Sarah.Davis@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 3, DaysOpen = 1 },
                    new Ticket { Title = "The network is down, and we can't do anything. Fix it now! This is a major disruption.", Priority = "Critical", CallerID = "james.hansen@farbrikam.com", State = "New", AssignedTo = "Daniel.Anderson@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 1), CompanyID = 1, DaysOpen = 0 },
                    new Ticket { Title = "I need a new laptop for my work. The current one is too slow.", Priority = "Medium", CallerID = "brandon.myers@wingtipstoys.com", State = "Resolved", AssignedTo = "Matthew.Jackson@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 4), CompanyID = 2, DaysOpen = 3 },
                    new Ticket { Title = "The application keeps crashing. I need this fixed immediately as it's hindering my work.", Priority = "High", CallerID = "evan.mercado@woodgrovebank.com", State = "In Progress", AssignedTo = "Joshua.Harris@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 4, DaysOpen = 1 },
                    new Ticket { Title = "Please back up my data. I have important files that need to be secured.", Priority = "Low", CallerID = "sarah.solis@wingtipstoys.com", State = "In Progress", AssignedTo = "Brandon.Hall@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 5), CompanyID = 2, DaysOpen = 4 },
                    new Ticket { Title = "We've had a security breach. This needs immediate attention to prevent data loss.", Priority = "Critical", CallerID = "charles.bauer@farbrikam.com", State = "Resolved", AssignedTo = "Christopher.Wright@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 1), CompanyID = 1, DaysOpen = 0 },
                    new Ticket { Title = "My account is locked, and I can't log in. Please unlock it as soon as possible.", Priority = "High", CallerID = "russell.marsh@farbrikam.com", State = "New", AssignedTo = "Justin.Baker@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 1, DaysOpen = 1 },
                    new Ticket { Title = "I need the latest software update installed. The current version is outdated.", Priority = "Medium", CallerID = "corey.wilson@woodgrovebank.com", State = "New", AssignedTo = "Kevin.Perez@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 3), CompanyID = 4, DaysOpen = 2 },
                    new Ticket { Title = "Emails are not being delivered. This is urgent as I am missing important communications.", Priority = "High", CallerID = "melissa.hall@woodgrovebank.com", State = "In Progress", AssignedTo = "Fatoumata.Diallo@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 4, DaysOpen = 1 },
                    new Ticket { Title = "The internet is very slow. Please check and resolve this issue.", Priority = "Medium", CallerID = "kathryn.nichols@wingtipstoys.com", State = "New", AssignedTo = "Mei.Ling@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 3), CompanyID = 2, DaysOpen = 2 },
                    new Ticket { Title = "I need access to the shared drive for team collaboration.", Priority = "Low", CallerID = "elizabeth.grant@woodgrovebank.com", State = "New", AssignedTo = "Hunter.Stephens@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 5), CompanyID = 4, DaysOpen = 4 },
                    new Ticket { Title = "The video conferencing tool isn't working. This is urgent as I have meetings scheduled.", Priority = "High", CallerID = "amy.johnson@woodgrovebank.com", State = "In Progress", AssignedTo = "Sharon.Compton@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 4, DaysOpen = 1 },
                    new Ticket { Title = "I need a new email account set up for a new employee.", Priority = "Medium", CallerID = "steven.martin@datumcorp.com", State = "Resolved", AssignedTo = "Sarah.Davis@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 4), CompanyID = 5, DaysOpen = 3 },
                    new Ticket { Title = "Please renew my software license. It is about to expire.", Priority = "Low", CallerID = "jennifer.martinez@datumcorp.com", State = "Resolved", AssignedTo = "Daniel.Anderson@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 5), CompanyID = 5, DaysOpen = 4 },
                    new Ticket { Title = "My computer is malfunctioning. I need this fixed immediately to continue my work.", Priority = "High", CallerID = "christopher.lee@datumcorp.com", State = "Resolved", AssignedTo = "Matthew.Jackson@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 5, DaysOpen = 1 },
                    new Ticket { Title = "I need remote desktop access set up to work from home.", Priority = "Medium", CallerID = "margaret.moon@woodgrovebank.com", State = "Resolved", AssignedTo = "Joshua.Harris@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 3), CompanyID = 4, DaysOpen = 2 },
                    new Ticket { Title = "The file sharing service isn't working. This is urgent as I need to share files with my team.", Priority = "High", CallerID = "vanessa.smith@woodgrovebank.com", State = "New", AssignedTo = "Brandon.Hall@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 4, DaysOpen = 1 },
                    new Ticket { Title = "I need more storage space for my projects.", Priority = "Low", CallerID = "chloe.butler@wingtipstoys.com", State = "New", AssignedTo = "Christopher.Wright@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 5), CompanyID = 2, DaysOpen = 4 },
                    new Ticket { Title = "Please set up the network printer. We need it for printing documents.", Priority = "Medium", CallerID = "ryan.lawrence@farbrikam.com", State = "In Progress", AssignedTo = "Justin.Baker@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 3), CompanyID = 1, DaysOpen = 2 },
                    new Ticket { Title = "I need training on the new software. Please schedule a session.", Priority = "Low", CallerID = "michael.liu@farbrikam.com", State = "In Progress", AssignedTo = "Kevin.Perez@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 5), CompanyID = 1, DaysOpen = 4 },
                    new Ticket { Title = "I can't access the cloud service. This is urgent as I need to retrieve files.", Priority = "High", CallerID = "javier.wright@adventureworks.com", State = "Resolved", AssignedTo = "Fatoumata.Diallo@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 3, DaysOpen = 1 },
                    new Ticket { Title = "Please create a new user account for a new team member.", Priority = "Medium", CallerID = "jessica.lyons@contoso.com", State = "New", AssignedTo = "Mei.Ling@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 4), CompanyID = 6, DaysOpen = 3 },
                    new Ticket { Title = "My email account has been hacked. This needs immediate attention to secure my data.", Priority = "Critical", CallerID = "karina.sanchez@contoso.com", State = "Resolved", AssignedTo = "Hunter.Stephens@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 1), CompanyID = 6, DaysOpen = 0 },
                    new Ticket { Title = "I need help with my mobile device. It isn't syncing with my work email.", Priority = "High", CallerID = "elaine.sharp@wingtipstoys.com", State = "Resolved", AssignedTo = "Sharon.Compton@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 2, DaysOpen = 1 },
                    new Ticket { Title = "I can't access the database. Please fix this as I need to retrieve information.", Priority = "Medium", CallerID = "earl.mitchell@datumcorp.com", State = "Resolved", AssignedTo = "Sarah.Davis@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 3), CompanyID = 5, DaysOpen = 2 },
                    new Ticket { Title = "Please set up VPN access for me. I need it for remote work.", Priority = "Low", CallerID = "sharon.cain@wingtipstoys.com", State = "New", AssignedTo = "Daniel.Anderson@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 5), CompanyID = 2, DaysOpen = 4 },
                    new Ticket { Title = "The software isn't compatible with my system. This is urgent as I need it for my work.", Priority = "High", CallerID = "jeffrey.torres@datumcorp.com", State = "Resolved", AssignedTo = "Matthew.Jackson@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 5, DaysOpen = 1 },
                    new Ticket { Title = "Please configure the network settings. We are experiencing connectivity issues.", Priority = "Medium", CallerID = "ian.singh@farbrikam.com", State = "Resolved", AssignedTo = "Joshua.Harris@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 4), CompanyID = 1, DaysOpen = 3 },
                    new Ticket { Title = "Remote access isn't working. This is urgent as I need to access my work files.", Priority = "High", CallerID = "jessica.lyons@contoso.com", State = "New", AssignedTo = "Brandon.Hall@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 2), CompanyID = 6, DaysOpen = 1 },
                    new Ticket { Title = "I need data recovery services. I accidentally deleted important files.", Priority = "Low", CallerID = "karina.sanchez@contoso.com", State = "Resolved", AssignedTo = "Christopher.Wright@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 5), CompanyID = 6, DaysOpen = 4 },
                    new Ticket { Title = "I can't open my email attachments again. This is the third time this month that the Outlook app is crashing and preventing me from work. This is super annoying!!!", Priority = "Medium", CallerID = "elaine.sharp@wingtipstoys.com", State = "New", AssignedTo = "Justin.Baker@CCSpark.io", Opened_at = new DateTime(2024, 12, 1), Closed_at = new DateTime(2024, 12, 3), CompanyID = 2, DaysOpen = 2 }
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
                    new Opportunity { ParentAccountId = 1, Account = "Contoso UK", Territory = "UK", ServiceLine = "Tech Source", Probability = 0.75, StageName = "Quote", Amount = 3750000, Currency = "£", Owner = "Cora Thomas", DateCreated = new DateTime(2024, 12, 2), CloseDate = new DateTime(2024, 12, 31) },
                    new Opportunity { ParentAccountId = 2, Account = "Fabrikam", Territory = "UK", ServiceLine = "Tech Source", Probability = 0.50, StageName = "Quote", Amount = 750000, Currency = "£", Owner = "Cora Thomas", DateCreated = new DateTime(2024, 12, 9), CloseDate = new DateTime(2024, 12, 31) },
                    new Opportunity { ParentAccountId = 3, Account = "Wingtips Toys", Territory = "UK", ServiceLine = "Tech Source", Probability = 1.00, StageName = "Quote", Amount = 1500000, Currency = "£", Owner = "Cora Thomas", DateCreated = new DateTime(2024, 11, 4), CloseDate = new DateTime(2024, 12, 31) }
                );
                context.SaveChanges();
            }
        }

        private static void SeedCustomers(CustomerDb context)
        {
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer { Id = 1, Name = "Contoso", AccountOwner = "Owner 1", Region = "Region 1" },
                    new Customer { Id = 2, Name = "Farbrikam", AccountOwner = "Owner 2", Region = "Region 2" },
                    new Customer { Id = 3, Name = "Wingtips Toys", AccountOwner = "Owner 3", Region = "Region 3" },
                    new Customer { Id = 5, Name = "Woodgrove Bank", AccountOwner = "Owner 4", Region = "Region 4" },
                    new Customer { Id = 6, Name = "Datum Corp", AccountOwner = "Owner 5", Region = "Region 5" },
                    new Customer { Id = 7, Name = "AdventureWorks", AccountOwner = "Owner 6", Region = "Region 6" }
                );
                context.SaveChanges();
            }
        }
    }
}