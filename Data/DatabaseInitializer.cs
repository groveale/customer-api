using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using groveale.Data;

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

                var orderDbContext = services.GetRequiredService<OrderDb>();
                orderDbContext.Database.EnsureCreated();


                var opportunityDbContext = services.GetRequiredService<OpportunityDb>();
                opportunityDbContext.Database.EnsureCreated();

                var accountDbContext = services.GetRequiredService<AccountDb>();
                accountDbContext.Database.EnsureCreated();
            }
        }
    }
}