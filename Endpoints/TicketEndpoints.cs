using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class TicketEndpoints
    {
        public static void MapTicketEndpoints(this IEndpointRouteBuilder routes)
        {
            var ticketItems = routes.MapGroup("/tickets").WithTags("Ticket");

            ticketItems.MapGet("/", GetAllTickets);
            ticketItems.MapGet("/{id}", GetTicketById);
            ticketItems.MapGet("/account/{accountId}", GetTicketsByAccount); // New endpoint
            ticketItems.MapPost("/", CreateTicket);
            ticketItems.MapPut("/{id}", UpdateTicket);
            ticketItems.MapDelete("/{id}", DeleteTicket);
        }

        public static async Task<IResult> GetAllTickets(TicketDb db) =>
            Results.Ok(await db.Tickets.ToListAsync());

        public static async Task<IResult> GetTicketById(int id, TicketDb db) =>
            await db.Tickets.FindAsync(id)
                is Ticket ticket
                    ? Results.Ok(ticket)
                    : Results.NotFound();

        public static async Task<IResult> GetTicketsByAccount(int accountId, TicketDb db) =>
            Results.Ok(await db.Tickets.Where(t => t.Account.Id == accountId).ToListAsync());

        public static async Task<IResult> CreateTicket(Ticket ticket, TicketDb db)
        {
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();
            return Results.Created($"/tickets/{ticket.Id}", ticket);
        }

        public static async Task<IResult> UpdateTicket(int id, Ticket inputTicket, TicketDb db)
        {
            var ticket = await db.Tickets.FindAsync(id);

            if (ticket is null) return Results.NotFound();

            ticket.Title = inputTicket.Title;
            ticket.Owner = inputTicket.Owner;
            ticket.CustomerServiceManager = inputTicket.CustomerServiceManager;
            ticket.CustomerName = inputTicket.CustomerName;
            ticket.CustomerContact = inputTicket.CustomerContact;
            ticket.Severity = inputTicket.Severity;
            ticket.Status = inputTicket.Status;
            ticket.DateOpened = inputTicket.DateOpened;
            ticket.DaysOpen = inputTicket.DaysOpen;
            ticket.Account = inputTicket.Account;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteTicket(int id, TicketDb db)
        {
            if (await db.Tickets.FindAsync(id) is Ticket ticket)
            {
                db.Tickets.Remove(ticket);
                await db.SaveChangesAsync();
                return Results.Ok(ticket);
            }

            return Results.NotFound();
        }
    }
}