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

            orderHeaderItems.MapGet("/", GetAllOrderHeaders);
        }

        public static async Task<IResult> GetAllOrderHeaders(OrderHeaderDb db) =>
            Results.Ok(await db.OrderHeaders.Take(5).ToListAsync());
    }
}