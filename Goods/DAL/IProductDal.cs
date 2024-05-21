using Goods.DAL.Models;

namespace Goods.DAL;

public interface IProductDal
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product> GetProductAsyncById(int id);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<List<Product>> GetProductsAsync();
}