using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class OpportunityEndpoints
    {
        public static void MapOpportunityEndpoints(this IEndpointRouteBuilder routes)
        {
            var opportunityItems = routes.MapGroup("/opportunities").WithTags("Opportunity");

            opportunityItems.MapGet("/", GetAllOpportunities);
            opportunityItems.MapGet("/{id}", GetOpportunityById);
            opportunityItems.MapPost("/", CreateOpportunity);
            opportunityItems.MapPut("/{id}", UpdateOpportunity);
            opportunityItems.MapDelete("/{id}", DeleteOpportunity);
            opportunityItems.MapGet("/account/{account}", GetOpportunitiesByAccountName); // New endpoint
            opportunityItems.MapGet("/parentAccount/{parentAccount}", GetOpportunitiesByParentAccountName); // New endpoint

            opportunityItems.MapGet("/{id}/orders", GetOrdersInOpportunityById);
        }

        public static async Task<IResult> GetOrdersInOpportunityById(int id, OpportunityDb db, OrderHeaderDb orderHeaderDb)
        {
            // get opp first
            var opportunity = await db.Opportunities.FindAsync(id);

            if (opportunity is null) return Results.NotFound();
    
            // get orders for opp
            var orders = await orderHeaderDb.OrderHeaders.Where(o => o.OpportunityID == id).ToListAsync();

            if (orders is null) return Results.NotFound();

            return Results.Ok(orders);
        }

        public static async Task<IResult> GetOpportunitiesByAccountName(string accountName, OpportunityDb db) =>
            Results.Ok(await db.Opportunities.Where(o => o.Account == accountName).ToListAsync());

        public static async Task<IResult> GetOpportunitiesByParentAccountName(string parentAccountName, OpportunityDb db) =>
            Results.Ok(await db.Opportunities.Where(o => o.ParentAccount == parentAccountName).ToListAsync());

        public static async Task<IResult> GetAllOpportunities(OpportunityDb db) =>
            Results.Ok(await db.Opportunities.ToListAsync());

        public static async Task<IResult> GetOpportunityById(int id, OpportunityDb db) =>
            await db.Opportunities.FindAsync(id)
                is Opportunity opportunity
                    ? Results.Ok(opportunity)
                    : Results.NotFound();

        public static async Task<IResult> CreateOpportunity(Opportunity opportunity, OpportunityDb db)
        {
            db.Opportunities.Add(opportunity);
            await db.SaveChangesAsync();
            return Results.Created($"/opportunities/{opportunity.Id}", opportunity);
        }

        public static async Task<IResult> UpdateOpportunity(int id, Opportunity inputOpportunity, OpportunityDb db)
        {
            var opportunity = await db.Opportunities.FindAsync(id);

            if (opportunity is null) return Results.NotFound();

            opportunity.Account = inputOpportunity.Account;
            opportunity.Territory = inputOpportunity.Territory;
            opportunity.Probability = inputOpportunity.Probability;
            opportunity.StageName = inputOpportunity.StageName;
            opportunity.Amount = inputOpportunity.Amount;
            opportunity.Currency = inputOpportunity.Currency;
            opportunity.Owner = inputOpportunity.Owner;
            opportunity.DateCreated = inputOpportunity.DateCreated;
            opportunity.CloseDate = inputOpportunity.CloseDate;
            opportunity.ServiceLine = inputOpportunity.ServiceLine;
            opportunity.ParentAccountId = inputOpportunity.ParentAccountId;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteOpportunity(int id, OpportunityDb db)
        {
            if (await db.Opportunities.FindAsync(id) is Opportunity opportunity)
            {
                db.Opportunities.Remove(opportunity);
                await db.SaveChangesAsync();
                return Results.Ok(opportunity);
            }

            return Results.NotFound();
        }
    }
}