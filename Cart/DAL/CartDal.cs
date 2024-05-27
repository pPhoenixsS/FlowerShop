using Cart.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.DAL;

public class CartDal : ICartDal
{
    public async Task RemoveProductAsync(params CartModel[] carts)
    {
        await using var db = new DbHelper();
        db.RemoveRange(carts.ToList());
        await db.SaveChangesAsync();
    }

    public async Task<List<CartModel>> GetCartByUserIdAsync(int userId)
    {
        await using var db = new DbHelper();
        var carts = await db.Carts.Where(c => c.UserId == userId).ToListAsync();
        return carts;
    }

    public async Task<CartModel> UpdateCartAsync(CartModel cart)
    {
        await using var db = new DbHelper();
        db.Carts.Update(cart);
        await db.SaveChangesAsync();
        return cart;
    }

    public async Task<CartModel> GetCartByIdAsync(int id)
    {
        await using var db = new DbHelper();
        var cartFromDb = await db.Carts.FindAsync(id)??new CartModel();
        return cartFromDb;
    }

    public async Task<CartModel> GetCartByUserAndProductAsync(int userId, int productId)
    {
        await using var db = new DbHelper();
        var cartFromDb =
            await db.Carts.FirstOrDefaultAsync(c => c.UserId == userId
                                                    && c.ProductId == productId) ?? new CartModel();
        return cartFromDb;
    }
}