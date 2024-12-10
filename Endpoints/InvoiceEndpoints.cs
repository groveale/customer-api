using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class InvoiceEndpoints
    {
        public static void MapInvoiceEndpoints(this IEndpointRouteBuilder routes)
        {
            var invoiceItems = routes.MapGroup("/invoices").WithTags("Invoice");

                
            invoiceItems.MapGet("/", GetAllInvoices);
            invoiceItems.MapGet("/{id}", GetInvoiceById);
            invoiceItems.MapPost("/", CreateInvoice);
            invoiceItems.MapPut("/{id}", UpdateInvoice);
            invoiceItems.MapDelete("/{id}", DeleteInvoice);
            invoiceItems.MapGet("/parentCustomer/{parentCustomerName}", GetInvoicesByParentCustomerName); // New endpoint
            invoiceItems.MapGet("/localCustomer/{localCustomerName}", GetInvoicesByLocalCustomerName); // New endpoint
            invoiceItems.MapGet("/salesDoc/{salesDocNumber}", GetInvoicesBySalesDocNumber); // New endpoint
        }

        public static async Task<IResult> GetAllInvoices(InvoiceDb db) =>
            Results.Ok(await db.Invoices.ToListAsync());

        public static async Task<IResult> GetInvoiceById(int id, InvoiceDb db) =>
            await db.Invoices.FindAsync(id)
                is Invoice invoice
                    ? Results.Ok(invoice)
                    : Results.NotFound();

        public static async Task<IResult> CreateInvoice(Invoice invoice, InvoiceDb db)
        {
            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();
            return Results.Created($"/invoices/{invoice.Id}", invoice);
        }

        public static async Task<IResult> UpdateInvoice(int id, Invoice inputInvoice, InvoiceDb db)
        {
            var invoice = await db.Invoices.FindAsync(id);

            if (invoice is null) return Results.NotFound();

            invoice.SalesDocNumber = inputInvoice.SalesDocNumber;
            invoice.InvoiceNumber = inputInvoice.InvoiceNumber;
            invoice.ParentCustomerID = inputInvoice.ParentCustomerID;
            invoice.ParentCustomer = inputInvoice.ParentCustomer;
            invoice.LocalCustomerID = inputInvoice.LocalCustomerID;
            invoice.LocalCustomer = inputInvoice.LocalCustomer;
            invoice.InvoiceDate = inputInvoice.InvoiceDate;
            invoice.PaymentDate = inputInvoice.PaymentDate;
            invoice.Currency = inputInvoice.Currency;
            invoice.TotalAmount = inputInvoice.TotalAmount;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteInvoice(int id, InvoiceDb db)
        {
            if (await db.Invoices.FindAsync(id) is Invoice invoice)
            {
                db.Invoices.Remove(invoice);
                await db.SaveChangesAsync();
                return Results.Ok(invoice);
            }

            return Results.NotFound();
        }

        public static async Task<IResult> GetInvoicesByParentCustomerName(string parentCustomerName, InvoiceDb db) =>
             Results.Ok(await db.Invoices.Where(i => i.ParentCustomer == parentCustomerName).ToListAsync());

        public static async Task<IResult> GetInvoicesByLocalCustomerName(string localCustomerName, InvoiceDb db) =>
             Results.Ok(await db.Invoices.Where(i => i.LocalCustomer == localCustomerName).ToListAsync());

        public static async Task<IResult> GetInvoicesBySalesDocNumber(string salesDocNumber, InvoiceDb db) =>
             Results.Ok(await db.Invoices.Where(i => i.SalesDocNumber == salesDocNumber).ToListAsync());
    }
}