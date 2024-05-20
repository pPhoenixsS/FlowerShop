using Goods.BLL;
using Goods.DAL.Models;
using Goods.Exceptions;
using Goods.Mappers;
using Goods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            await productBll.DeleteProductAsync(id);
            await imagesBll.DeleteImagesAsync(await imagesBll.GetImagesByProductAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
}