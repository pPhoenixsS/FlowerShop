using Goods.DAL.Models;

namespace Goods.Mappers;

public static class ProductQmToProductModel
{
    public static Product Map(ProductQueryModel queryModel)
    {
        var product = new Product()
        {
            Count = queryModel.Count,
            Description = queryModel.Description,
            Kind = queryModel.Kind,
            Name = queryModel.Name,
            Price = queryModel.Price
        };

        return product;
    }
}