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

            // tickets by customer name
            
            //ticketItems.MapGet("/", GetAllTickets);
            ticketItems.MapGet("/{id}", GetTicketById);
            ticketItems.MapGet("/customer/{customerName}", GetTicketsByCustomerName);
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

        public static async Task<IResult> GetTicketsByCustomerName(string customerName, TicketDb db){
            return Results.Ok(await db.Tickets.Where(t => t.CompanyName == customerName).ToListAsync());
        }
            
           

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

            ticket.State = inputTicket.State;
            ticket.Priority = inputTicket.Priority;
            ticket.Long_Description = inputTicket.Long_Description;
            ticket.AssignedTo = inputTicket.AssignedTo;
            ticket.Opened_at = inputTicket.Opened_at;
            ticket.Closed_at = inputTicket.Closed_at;
            ticket.CompanyID = inputTicket.CompanyID;
            ticket.DaysOpen = inputTicket.DaysOpen;
            ticket.CallerID = inputTicket.CallerID; 
            ticket.CompanyName = inputTicket.CompanyName;
            ticket.Short_Description = inputTicket.Short_Description;

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