using Cart.DAL.Models;

namespace Cart.DAL;

public interface ICartDal
{
    public Task<CartModel> AddProductAsync(CartModel cart);
    public Task RemoveProductAsync(CartModel cart);
    public Task<List<CartModel>> GetCartByUserIdAsync(int userId);
    public Task<CartModel> UpdateCartAsync(CartModel cart);
}