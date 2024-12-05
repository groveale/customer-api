using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder routes)
        {
            var customerItems = routes.MapGroup("/customer").WithTags("Customer");

            customerItems.MapGet("/", GetAllCustomers);
            customerItems.MapGet("/{id}", GetCustomerById);
            customerItems.MapPost("/", CreateCustomer);
            customerItems.MapPut("/{id}", UpdateCustomer);
            customerItems.MapDelete("/{id}", DeleteCustomer);
        }

        public static async Task<IResult> GetAllCustomers(CustomerDb db) =>
            Results.Ok(await db.Customers.ToListAsync());

        public static async Task<IResult> GetCustomerById(int id, CustomerDb db) =>
            await db.Customers.FindAsync(id)
                is Customer customer
                    ? Results.Ok(customer)
                    : Results.NotFound();

        public static async Task<IResult> CreateCustomer(Customer customer, CustomerDb db)
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return Results.Created($"/customer/{customer.Id}", customer);
        }

        public static async Task<IResult> UpdateCustomer(int id, Customer inputCustomer, CustomerDb db)
        {
            var customer = await db.Customers.FindAsync(id);

            if (customer is null) return Results.NotFound();

            customer.Name = inputCustomer.Name;
            customer.Strategic = inputCustomer.Strategic;
            customer.AccountOwner = inputCustomer.AccountOwner;
            customer.SellerID = inputCustomer.SellerID;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteCustomer(int id, CustomerDb db)
        {
            if (await db.Customers.FindAsync(id) is Customer customer)
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
                return Results.Ok(customer);
            }

            return Results.NotFound();
        }
    }
}