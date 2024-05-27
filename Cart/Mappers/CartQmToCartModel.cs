using Cart.DAL.Models;

namespace Cart.Mappers;

public static class CartQmToCartModel
{
    public static CartModel Map(CartQueryModel cartQueryModel, int userId)
    {
        return new CartModel()
        {
            Count = cartQueryModel.Count,
            ProductId = cartQueryModel.ProductId,
            UserId = userId
        };
    }
}