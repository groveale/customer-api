using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class OrderHeaderEndpoints
    {
        public static void MapOrderHeaderEndpoints(this IEndpointRouteBuilder routes)
        {
            var orderHeaderItems = routes.MapGroup("/orderheaders").WithTags("OrderHeader");

            orderHeaderItems.MapGet("/", GetAllOrders);
            orderHeaderItems.MapGet("/{id}", GetOrderById);
            orderHeaderItems.MapPost("/", CreateOrder);
            orderHeaderItems.MapPut("/{id}", UpdateOrder);
            orderHeaderItems.MapDelete("/{id}", DeleteOrder);

            orderHeaderItems.MapGet("/parentCustomer/{parentCustomer}", GetAllOrdersByCustomerName);
            orderHeaderItems.MapGet("/localCustomer/{localCustomer}", GetAllOrdersByLocalCustomerName);

            orderHeaderItems.MapGet("/opportunity/{opportunityId}", GetAllOrdersByOpportunityId);
        }


        public static async Task<IResult> GetAllOrdersByOpportunityId(int opportunityId, OrderHeaderDb db) =>
            Results.Ok(await db.OrderHeaders.Where(o => o.OpportunityID == opportunityId).ToListAsync());


        public static async Task<IResult> GetAllOrders(OrderHeaderDb db) =>
            Results.Ok(await db.OrderHeaders.ToListAsync());

        public static async Task<IResult> GetOrderById(int id, OrderHeaderDb db) =>
            await db.OrderHeaders.FindAsync(id)
                is OrderHeader order
                    ? Results.Ok(order)
                    : Results.NotFound();

        public static async Task<IResult> CreateOrder(OrderHeader order, OrderHeaderDb db)
        {
            db.OrderHeaders.Add(order);
            await db.SaveChangesAsync();
            return Results.Created($"/orderheaders/{order.Id}", order);
        }

        public static async Task<IResult> UpdateOrder(int id, OrderHeader inputOrder, OrderHeaderDb db)
        {
            var order = await db.OrderHeaders.FindAsync(id);

            if (order is null) return Results.NotFound();

            order.SalesDocNumber = inputOrder.SalesDocNumber;
            order.LocalCustomer = inputOrder.LocalCustomer;
            order.ParentCustomer = inputOrder.ParentCustomer;
            order.OpportunityID = inputOrder.OpportunityID;
            order.SalesDocType = inputOrder.SalesDocType;
            order.SalesOrganisation = inputOrder.SalesOrganisation;
            order.DistributionChannel = inputOrder.DistributionChannel;
            order.ParentCustomerID = inputOrder.ParentCustomerID;
            order.LocalCustomerID = inputOrder.LocalCustomerID;
            order.DateCreated = inputOrder.DateCreated;
            order.CreatedBy = inputOrder.CreatedBy;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteOrder(int id, OrderHeaderDb db)
        {
            if (await db.OrderHeaders.FindAsync(id) is OrderHeader order)
            {
                db.OrderHeaders.Remove(order);
                await db.SaveChangesAsync();
                return Results.Ok(order);
            }

            return Results.NotFound();
        }

        public static async Task<IResult> GetAllOrdersByCustomerName(string parentCustomer, OrderHeaderDb db) =>
            Results.Ok(await db.OrderHeaders.Where(o => o.ParentCustomer == parentCustomer).ToListAsync());

        public static async Task<IResult> GetAllOrdersByLocalCustomerName(string localCustomer, OrderHeaderDb db) =>
            Results.Ok(await db.OrderHeaders.Where(o => o.LocalCustomer == localCustomer).ToListAsync());


    }
}