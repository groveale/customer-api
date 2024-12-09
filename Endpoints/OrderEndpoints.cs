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

            orderItems.MapGet("/customer/{localCustomerName}", GetOrdersByLocalCustomerName);
            orderItems.MapGet("/parentCustomer/{parentCustomerName}", GetOrdersByParentCustomerName);
            orderItems.MapGet("/salesOrder/{salesOrderId}", GetOrdersBySalesOrderId);
        }

        public static async Task<IResult> GetAllOrders(OrderDb db) =>
            Results.Ok(await db.Orders.ToListAsync());

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

            order.ParentCustomerID = inputOrder.ParentCustomerID;
            order.ParentCustomer = inputOrder.ParentCustomer;
            order.LocalCustomer = inputOrder.LocalCustomer;
            order.LocalCustomerID = inputOrder.LocalCustomerID;
            order.SalesOrderID = inputOrder.SalesOrderID;
            order.UnitQuantity = inputOrder.UnitQuantity;
            order.ItemValue = inputOrder.ItemValue;
            order.StorageLocation = inputOrder.StorageLocation;
            order.OrderItemName = inputOrder.OrderItemName;
            order.OrderItemDescription = inputOrder.OrderItemDescription;
            order.ShippingDestinationCountry = inputOrder.ShippingDestinationCountry;
            order.ShippingDestinationCity = inputOrder.ShippingDestinationCity;
            order.ProfitCentre = inputOrder.ProfitCentre;

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

        public static async Task<IResult> GetOrdersByLocalCustomerName(string localCustomerName, OrderDb db) =>
                Results.Ok(await db.Orders.Where(o => o.LocalCustomer == localCustomerName).ToListAsync());

        public static async Task<IResult> GetOrdersByParentCustomerName(string parentCustomerName, OrderDb db) =>
            Results.Ok(await db.Orders.Where(o => o.ParentCustomer == parentCustomerName).ToListAsync());

        public static async Task<IResult> GetOrdersBySalesOrderId(string salesOrderId, OrderDb db) =>
            Results.Ok(await db.Orders.Where(o => o.SalesOrderID == salesOrderId).ToListAsync());
    }
}