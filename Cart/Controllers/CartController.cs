using Cart.BLL;
using Cart.DAL.Models;
using Cart.Exceptions;
using Cart.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Controllers;

[Authorize]
[ApiController]
public class CartController(ICartBll cartBll) : ControllerBase
{
    [HttpPost("/cart")]
    public async Task<IActionResult> AddProductAsync([FromBody] CartQueryModel cartQueryModel)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null)
            return BadRequest();
        
        var userId = int.Parse(userIdClaim.Value);
        
        var cart = CartQmToCartModel.Map(cartQueryModel, userId);

        await cartBll.UpdateCartAsync(cart, userId);

        List<CartModel> carts = new List<CartModel>();

        try
        {
            carts = await cartBll.GetCartByUserIdAsync(userId);
        }
        catch (Exception e)
        {
            return Ok(carts);
        }

        
        return Ok(carts);
    }

    [HttpGet("/cart")]
    public async Task<IActionResult> GetCart()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null)
            return BadRequest();
        var userId = int.Parse(userIdClaim.Value);

        List<CartModel> carts;
        
        try
        {
            carts = await cartBll.GetCartByUserIdAsync(userId);
        }
        catch (NotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok(carts);
    }

    [HttpDelete("/cart")]
    public async Task<IActionResult> RemoveCart()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null)
            return BadRequest();
        var userId = int.Parse(userIdClaim.Value);
        try
        {
            await cartBll.RemoveCartByUserId(userId);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
}