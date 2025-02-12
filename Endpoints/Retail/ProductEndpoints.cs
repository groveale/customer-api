using groveale.Data;
using groveale.Models;
using Microsoft.EntityFrameworkCore;

namespace groveale.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
        {
            var productItems = routes.MapGroup("/product").WithTags("Product");

            productItems.MapGet("/store/{storeNumber}", GetProducstByStoreNumber); // New endpoint
            productItems.MapGet("/high/damage", GetHighDamageProducts); // New endpoint
            productItems.MapGet("/high/theft", GetHighTheftProducts); // New endpoint
            productItems.MapGet("/eol/fresh", GetNearingEndOfLifeFreshProducts); // New endpoint
            productItems.MapGet("/eol/shelf", GetNearingEndOfLifeShelfProducts); // New endpoint
            productItems.MapGet("/{sku}", GetProductBySKU); // New endpoint
            productItems.MapGet("/{sku}/damage/{damageCount}", UpdateDamageCountBySKU); // New endpoint


        }

        public static async Task<IResult> GetProducstByStoreNumber(int storeNumber, ProductDb db) =>
            Results.Ok(await db.Products.Where(o => o.StoreNumber == storeNumber).ToListAsync());

        public static async Task<IResult> GetHighDamageProducts(ProductDb db) =>
            Results.Ok(await db.Products.OrderByDescending(p => p.Damaged).Take(5).ToListAsync());

        public static async Task<IResult> GetHighTheftProducts(ProductDb db) =>
            Results.Ok(await db.Products.OrderByDescending(p => p.Stolen).Take(5).ToListAsync());

        public static async Task<IResult> GetNearingEndOfLifeFreshProducts(ProductDb db) =>
            Results.Ok(await db.Products.Where(p => p.Type.ToLowerInvariant() == "fresh" && p.EoLDays <= 7).ToListAsync());

        public static async Task<IResult> GetNearingEndOfLifeShelfProducts(ProductDb db) =>
            Results.Ok(await db.Products.Where(p => p.Type.ToLowerInvariant() == "shelf" && p.EoLDays <= 30).ToListAsync());
    
        public static async Task<IResult> GetProductBySKU(string sku, ProductDb db) =>
            await db.Products.Where(p => p.SKU == sku).FirstOrDefaultAsync() is Product product
                ? Results.Ok(product)
                : Results.NotFound();

        public static async Task<IResult> UpdateDamageCountBySKU(string sku, int damageCount, ProductDb db)
        {
            var existing = await db.Products.Where(p => p.SKU == sku).FirstOrDefaultAsync();
            if (existing == null) return Results.NotFound();
            existing.Damaged += damageCount;
            await db.SaveChangesAsync();
            return Results.Ok(existing);
        }
    }
}