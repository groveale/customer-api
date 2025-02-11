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

            productItems.MapGet("/", GetAllProducts);
            productItems.MapGet("/{sku}", GetProductBySKU);
            productItems.MapPost("/", CreateProduct);
            productItems.MapPut("/{sku}", UpdateProduct);
            productItems.MapDelete("/{sku}", DeleteProduct);
            productItems.MapGet("/store/{storeNumber}", GetProducstByStoreNumber); // New endpoint
             productItems.MapGet("/high-damage", GetHighDamageProducts); // New endpoint
            productItems.MapGet("/high-theft", GetHighTheftProducts); // New endpoint
            productItems.MapGet("/nearing-end-of-life-fresh", GetNearingEndOfLifeFreshProducts); // New endpoint
            productItems.MapGet("/nearing-end-of-life-shelf", GetNearingEndOfLifeShelfProducts); // New endpoint

        }

        public static async Task<IResult> GetProducstByStoreNumber(int storeNumber, ProductDb db) =>
            Results.Ok(await db.Products.Where(o => o.StoreNumber == storeNumber).ToListAsync());


        public static async Task<IResult> GetAllProducts(ProductDb db) =>
            Results.Ok(await db.Products.ToListAsync());

        public static async Task<IResult> GetProductBySKU(int id, ProductDb db) =>
            await db.Products.FindAsync(id)
                is Product product
                    ? Results.Ok(product)
                    : Results.NotFound();

        public static async Task<IResult> CreateProduct(Product product, ProductDb db)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/product/{product.SKU}", product);
        }

        public static async Task<IResult> UpdateProduct(int id, Product inputProduct, ProductDb db)
        {
            var product = await db.Products.FindAsync(id);

            if (product is null) return Results.NotFound();

            product.SKU = inputProduct.SKU;
            product.Sold = inputProduct.Sold;
            product.InStock = inputProduct.InStock;
            product.Stolen = inputProduct.Stolen;
            product.Damaged = inputProduct.Damaged;
            product.EoLDays = inputProduct.EoLDays;
            product.CostPrice = inputProduct.CostPrice;
            product.SoldPrice = inputProduct.SoldPrice;
            product.StoreNumber = inputProduct.StoreNumber;
            product.Type = inputProduct.Type;


            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteProduct(int id, ProductDb db)
        {
            if (await db.Products.FindAsync(id) is Product product)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }

            return Results.NotFound();
        }

        public static async Task<IResult> GetHighDamageProducts(ProductDb db) =>
    Results.Ok(await db.Products.OrderByDescending(p => p.Damaged).Take(5).ToListAsync());

public static async Task<IResult> GetHighTheftProducts(ProductDb db) =>
    Results.Ok(await db.Products.OrderByDescending(p => p.Stolen).Take(5).ToListAsync());

        public static async Task<IResult> GetNearingEndOfLifeFreshProducts(ProductDb db) =>
            Results.Ok(await db.Products.Where(p => p.Type.ToLowerInvariant() == "fresh" && p.EoLDays <= 7).ToListAsync());

        public static async Task<IResult> GetNearingEndOfLifeShelfProducts(ProductDb db) =>
            Results.Ok(await db.Products.Where(p => p.Type.ToLowerInvariant() == "shelf" && p.EoLDays <= 30).ToListAsync());
    }
}