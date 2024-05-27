using Cart.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.DAL;

public class CartDal : ICartDal
{
    public async Task<CartModel> AddProductAsync(CartModel cart)
    {
        await using var db = new DbHelper();
        await db.Carts.AddAsync(cart);
        await db.SaveChangesAsync();
        return cart;
    }

    public async Task RemoveProductAsync(CartModel cart)
    {
        await using var db = new DbHelper();
        db.Remove(cart);
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
        return cart;
    }

    public async Task<CartModel> GetCartByIdAsync(int id)
    {
        await using var db = new DbHelper();
        var cartFromDb = await db.Carts.FindAsync(id)??new CartModel();
        return cartFromDb;
    }
}