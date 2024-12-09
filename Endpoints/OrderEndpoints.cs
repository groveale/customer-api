using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder routes)
        {
            var orderHeaderItems = routes.MapGroup("/orderheaders").WithTags("OrderHeader");

            orderHeaderItems.MapGet("/parentCustomer/{parentCustomer}", GetAllOrdersByCustomerName);
            orderHeaderItems.MapGet("/localCustomer/{localCustomer}", GetAllOrdersByLocalCustomerName);

            orderHeaderItems.MapGet("/{id}/orderitems", GetItemsInOrderById);
            orderHeaderItems.MapGet("/{salesDocNumber}/orderitems", GetItemsInOrderBySalesDocNumber);
        }

        public static async Task<IResult> GetItemsInOrderById(int id, OrderHeaderDb db, OrderDb orderDb)
        {
            // get order first
            var order = await db.OrderHeaders.FindAsync(id);

            if (order is null) return Results.NotFound();

            // get items for order
            var items = await orderDb.Orders.Where(o => o.SalesOrderID == order.SalesDocNumber).ToListAsync();

            if (items is null) return Results.NotFound();

            return Results.Ok(items);
        }

        public static async Task<IResult> GetItemsInOrderBySalesDocNumber(string salesDocNumber, OrderHeaderDb db, OrderDb orderDb)
        {
            // get order first
            var order = await db.OrderHeaders.FirstOrDefaultAsync(o => o.SalesDocNumber == salesDocNumber);

            if (order is null) return Results.NotFound();

            // get items for order
            var items = await orderDb.Orders.Where(o => o.SalesOrderID == salesDocNumber).ToListAsync();

            if (items is null) return Results.NotFound();

            return Results.Ok(items);
        }

        public static async Task<IResult> GetAllOrdersByCustomerName(string customerName, OrderHeaderDb db) =>
            Results.Ok(await db.OrderHeaders.Where(o => o.ParentCustomer == customerName).ToListAsync());

        public static async Task<IResult> GetAllOrdersByLocalCustomerName(string localCustomerName, OrderHeaderDb db) =>
            Results.Ok(await db.OrderHeaders.Where(o => o.LocalCustomer == localCustomerName).ToListAsync());

        
    }
}