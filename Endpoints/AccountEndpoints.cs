using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class AccountEndpoints
    {
        public static void MapAccountEndpoints(this IEndpointRouteBuilder routes)
        {
            var accountItems = routes.MapGroup("/accounts").WithTags("Account");

            accountItems.MapGet("/", GetAllAccounts);
            accountItems.MapGet("/{id}", GetAccountById);
            accountItems.MapPost("/", CreateAccount);
            accountItems.MapPut("/{id}", UpdateAccount);
            accountItems.MapDelete("/{id}", DeleteAccount);
        }

        public static async Task<IResult> GetAllAccounts(AccountDb db) =>
            Results.Ok(await db.Accounts.ToListAsync());

        public static async Task<IResult> GetAccountById(int id, AccountDb db) =>
            await db.Accounts.FindAsync(id)
                is Account account
                    ? Results.Ok(account)
                    : Results.NotFound();

        public static async Task<IResult> CreateAccount(Account account, AccountDb db)
        {
            db.Accounts.Add(account);
            await db.SaveChangesAsync();
            return Results.Created($"/accounts/{account.Id}", account);
        }

        public static async Task<IResult> UpdateAccount(int id, Account inputAccount, AccountDb db)
        {
            var account = await db.Accounts.FindAsync(id);

            if (account is null) return Results.NotFound();

            account.Name = inputAccount.Name;
            account.Strategic = inputAccount.Strategic;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteAccount(int id, AccountDb db)
        {
            if (await db.Accounts.FindAsync(id) is Account account)
            {
                db.Accounts.Remove(account);
                await db.SaveChangesAsync();
                return Results.Ok(account);
            }

            return Results.NotFound();
        }
    }
}