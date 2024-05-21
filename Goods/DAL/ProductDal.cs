using Goods.DAL.Models;

namespace Goods.DAL;

public class ProductDal : IProductDal
{
    public async Task<Product> CreateProductAsync(Product product)
    {
        await using var db = new DbHelper();
        await db.Products.AddAsync(product);
        await db.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetProductAsyncById(int id)
    {
        await using var db = new DbHelper();
        var productFromDb = await db.Products.FindAsync(id) ?? new Product();
        return productFromDb;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        await using var db = new DbHelper();
        db.Products.Update(product);
        await db.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(int id)
    {
        await using var db = new DbHelper();
        db.Products.Remove(await GetProductAsyncById(id));
        await db.SaveChangesAsync();
    }
}