using BonusSystem.BLL;
using BonusSystem.DAL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BonusSystem.Controllers;

[ApiController]
public class BonusController(IBonusBll bonusBll) : ControllerBase
{
    [HttpPost("/bonus")]
    public async Task<IActionResult> CreateAsync()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null)
            return BadRequest();
        var userId = int.Parse(userIdClaim.Value);
        
        try
        {
            await bonusBll.CreateAsync(userId);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet("/bonus")]
    public async Task<IActionResult> GetBonuses()
    {
        Bonus bonus;
        
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null)
            return BadRequest();
        var userId = int.Parse(userIdClaim.Value);
        
        try
        {
            bonus = await bonusBll.GetBonusAsync(userId);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok(bonus);
    }

    [HttpPut("/bonus/{count:int}")]
    public async Task<IActionResult> UpdateBonus(int count)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null)
            return BadRequest();
        var userId = int.Parse(userIdClaim.Value);
        
        try
        {
            await bonusBll.UpdateBonuses(userId, count);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
}