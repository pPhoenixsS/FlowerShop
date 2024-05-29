using System.Text.Json.Serialization;
using Goods.BLL;
using Goods.DAL.Models;
using Goods.Exceptions;
using Goods.Mappers;
using Goods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Goods.Controllers;

public class ProductController(IProductBll productBll, IImagesBll imagesBll) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPost("/addproduct")]
    public async Task<IActionResult> AddProduct([FromForm] ProductQueryModel productQueryModel)
    {
        var product = ProductQmToProductModel.Map(productQueryModel);
        
        try
        {
            await productBll.CreateProductAsync(product);
            await imagesBll.AddImagesAsync(product.Id, productQueryModel.Images); 
        }
        catch (BadImageException)
        {
            return BadRequest();
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok(product);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/deleteproduct/{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            await imagesBll.DeleteImagesAsync(await imagesBll.GetImagesByProductAsync(id));
            await productBll.DeleteProductAsync(id);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Authorize]
    [HttpGet("/product/{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        Product product;
        List<Images> images;

        try
        {
            var productTask = productBll.GetProductAsyncById(id);
            var imagesTask = imagesBll.GetImagesByProductAsync(id);
            product = await productTask;
            images = await imagesTask;
        }
        catch (NotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        var convertedImages = await ImageManager.ImageToBase64(images);

        var response = new
        {
            Id = product.Id,
            Name = product.Name,
            Kind = product.Kind,
            Description = product.Description,
            Price = product.Price,
            Count = product.Count,
            Images = convertedImages
        };

        return Ok(response);
    }
    
    [Authorize]
    [HttpGet("/products")]
    public async Task<IActionResult> GetProducts()
    {
        List<ProductResponceModel> products = new List<ProductResponceModel>(); 
        
        try
        {
            var task = await Task.Factory.StartNew(async () =>
            {
                var productsFromDb = await productBll.GetProductsAsync();
                foreach (var product in productsFromDb)
                {
                    var images = await ImageManager.ImageToBase64(product.Images);
                    products.Add(new ProductResponceModel()
                    {
                        Id = product.Id,
                        Count = product.Count,
                        Description = product.Description,
                        Images = images,
                        Kind = product.Kind,
                        Name = product.Name,
                        Price = product.Price
                    });
                }
            }, TaskCreationOptions.LongRunning);
            await task;
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        var json = JsonConvert.SerializeObject(products);
        
        return Ok(json);
    }

    [Authorize]
    [HttpGet("/buy")]
    public async Task<IActionResult> BuyProduct()
    {
        var jwt = Request.Headers.Authorization;
        try
        {
            await productBll.BuyProducts(jwt.ToString());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
}
