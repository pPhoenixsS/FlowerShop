using Cart.DAL;
using Cart.DAL.Models;
using Cart.Exceptions;

namespace Cart.BLL;

public class CartBll(ICartDal cartDal) : ICartBll
{
    public async Task<CartModel> AddProductAsync(CartModel cart)
    {
        var cartFromDb = await cartDal.GetCartByIdAsync(cart.Id);
        if (cartFromDb.Id != 0)
        {
            await UpdateCartAsync(cart);
            return cart;
        }

        await cartDal.AddProductAsync(cart);
        return cart;
    }

    public async Task RemoveProductAsync(CartModel cart)
    {
        var cartFromDb = await cartDal.GetCartByIdAsync(cart.Id);
        if (cartFromDb.Id == 0)
        {
            throw new NotFoundException();
        }

        await cartDal.RemoveProductAsync(cart);
    }

    public async Task<List<CartModel>> GetCartByUserIdAsync(int userId)
    {
        var cart = await cartDal.GetCartByUserIdAsync(userId);
        if (cart.Count == 0)
            throw new NotFoundException();

        return cart;
    }

    public async Task<CartModel> UpdateCartAsync(CartModel cart)
    {
        var cartFromDb = await cartDal.GetCartByIdAsync(cart.Id);
        if (cartFromDb.Id == 0)
        {
            throw new NotFoundException();
        }

        await cartDal.UpdateCartAsync(cart);
        return cart;
    }
}