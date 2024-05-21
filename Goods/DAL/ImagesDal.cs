using Goods.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Goods.DAL;

public class ImagesDal : IImagesDal
{
    public async Task<List<Images>> AddImagesAsync(List<Images> images)
    {
        await using var db = new DbHelper();
        await db.Images.AddRangeAsync(images);
        await db.SaveChangesAsync();
        return images;
    }

    public async Task DeleteImagesAsync(List<Images> images)
    {
        await using var db = new DbHelper();
        db.Images.RemoveRange(images);
        await db.SaveChangesAsync();
    }

    public async Task<List<Images>> GetImagesByProductAsync(int productId)
    {
        await using var db = new DbHelper();
        var products = await db.Images.Where(p => p.ProductId == productId).ToListAsync();
        return products;
    }
}