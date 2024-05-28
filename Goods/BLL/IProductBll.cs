using Goods.DAL.Models;

namespace Goods.BLL;

public interface IProductBll
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product> GetProductAsyncById(int id);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<List<Product>> GetProductsAsync();
    Task BuyProducts(string jwt);
}