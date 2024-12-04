using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder routes)
        {
            var orderItems = routes.MapGroup("/orders").WithTags("Order");

            orderItems.MapGet("/", GetAllOrders);
            orderItems.MapGet("/{id}", GetOrderById);
            orderItems.MapPost("/", CreateOrder);
            orderItems.MapPut("/{id}", UpdateOrder);
            orderItems.MapDelete("/{id}", DeleteOrder);
             orderItems.MapGet("/account/{accountName}", GetOrdersByAccountName); // New endpoint
        }

        public static async Task<IResult> GetAllOrders(OrderDb db) =>
            Results.Ok(await db.Orders.ToListAsync());

        public static async Task<IResult> GetOrdersByAccountName(string accountName, OrderDb db) =>
            Results.Ok(await db.Orders.Where(o => o.AccountName == accountName).ToListAsync());

        public static async Task<IResult> GetOrderById(int id, OrderDb db) =>
            await db.Orders.FindAsync(id)
                is Order order
                    ? Results.Ok(order)
                    : Results.NotFound();

        public static async Task<IResult> CreateOrder(Order order, OrderDb db)
        {
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return Results.Created($"/orders/{order.Id}", order);
        }

        public static async Task<IResult> UpdateOrder(int id, Order inputOrder, OrderDb db)
        {
            var order = await db.Orders.FindAsync(id);

            if (order is null) return Results.NotFound();

            order.Title = inputOrder.Title;
            order.AccountName = inputOrder.AccountName;
            order.Territory = inputOrder.Territory;
            order.Status = inputOrder.Status;
            order.OrderValue = inputOrder.OrderValue;
            order.Currency = inputOrder.Currency;
            order.Products = inputOrder.Products;
            order.DateCreated = inputOrder.DateCreated;
            order.DateClosed = inputOrder.DateClosed;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteOrder(int id, OrderDb db)
        {
            if (await db.Orders.FindAsync(id) is Order order)
            {
                db.Orders.Remove(order);
                await db.SaveChangesAsync();
                return Results.Ok(order);
            }

            return Results.NotFound();
        }
    }
}