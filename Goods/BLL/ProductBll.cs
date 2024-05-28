using Goods.DAL;
using Goods.DAL.Models;
using Goods.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

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

    public async Task<List<Product>> GetProductsAsync()
    {
        var products = await productDal.GetProductsAsync();
        return products;
    }

    public async Task BuyProducts(string jwt)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", jwt);
        var response = await client.GetAsync("http://localhost:5002/cart");
        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var content = await response.Content.ReadAsStringAsync();
        
        var cart = JsonConvert.DeserializeObject<List<CartModel>>(content);

        if (cart.Count == 0)
            throw new Exception();

        List<Product> productsFromDb = new List<Product>();

        foreach (var product in cart)
        {
            var productFromDb = await productDal.GetProductAsyncById(product.ProductId);
            productsFromDb.Add(productFromDb);
            if (productFromDb.Count < product.Count)
                throw new Exception("недостаточно товара");
        }

        foreach (var product in cart)
        {
            var productFromDb = productsFromDb.FirstOrDefault(p => p.Id == product.ProductId) ?? new Product();
            if(productFromDb.Id==0)
                throw new Exception("недостаточно товара");
            productFromDb.Count -= product.Count;
            await productDal.UpdateProductAsync(productFromDb);
        }

        await client.DeleteAsync("http://localhost:5002/cart");
    }
}