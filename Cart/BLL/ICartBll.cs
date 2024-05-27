using Cart.DAL.Models;

namespace Cart.BLL;

public interface ICartBll
{
    public Task<CartModel> AddProductAsync(CartModel cart);
    public Task RemoveProductAsync(CartModel cart);
    public Task<List<CartModel>> GetCartByUserIdAsync(int userId);
    public Task<CartModel> UpdateCartAsync(CartModel cart);
}