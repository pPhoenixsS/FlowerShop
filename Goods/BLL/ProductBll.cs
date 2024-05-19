using Goods.DAL;
using Goods.DAL.Models;
using Goods.Exceptions;

namespace Goods.BLL;

public class ProductBll(IProductDal productDal) : IProductBll
{
    public async Task<Product> CreateProductAsync(Product product)
    {
        await productDal.CreateProductAsync(product);
        return product;
    }

    public async Task<Product> GetProductAsyncById(int id)
    {
        var productFromDb = await productDal.GetProductAsyncById(id);
        if (productFromDb.Id == 0)
            throw new NotFoundException();
        return productFromDb;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        await productDal.UpdateProductAsync(product);
        return product;
    }

    public async Task DeleteProductAsync(int id)
    {
        if ((await productDal.GetProductAsyncById(id)).Id == 0)
            throw new NotFoundException();
        await productDal.DeleteProductAsync(id);
    }
}