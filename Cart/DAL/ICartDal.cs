using Cart.DAL.Models;

namespace Cart.DAL;

public interface ICartDal
{
    public Task RemoveProductAsync(params CartModel[] carts);
    public Task<List<CartModel>> GetCartByUserIdAsync(int userId);
    public Task<CartModel> UpdateCartAsync(CartModel cart);
    public Task<CartModel> GetCartByIdAsync(int id);
    public Task<CartModel> GetCartByUserAndProductAsync(int userId, int productId);
}