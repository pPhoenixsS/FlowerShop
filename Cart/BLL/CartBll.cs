using Cart.DAL;
using Cart.DAL.Models;
using Cart.Exceptions;

namespace Cart.BLL;

public class CartBll(ICartDal cartDal) : ICartBll
{
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

    public async Task<CartModel> UpdateCartAsync(CartModel cart, int userId)
    {
        var cartFromDb = await cartDal.GetCartByUserAndProductAsync(userId, cart.ProductId);
        
        if (cartFromDb.Id != 0)
        {
            cart.Id = cartFromDb.Id;
        }

        if (cart.Count == 0)
        {
            await cartDal.RemoveProductAsync(cartFromDb);
            return cart;
        }

        await cartDal.UpdateCartAsync(cart);
        return cart;
    }

    public async Task RemoveCartByUserId(int userId)
    {
        var cart = (await cartDal.GetCartByUserIdAsync(userId)).ToArray();

        await cartDal.RemoveProductAsync(cart);
    }
}