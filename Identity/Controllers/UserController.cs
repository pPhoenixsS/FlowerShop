using Identity.BLL;
using Identity.DAL.Models;
using Identity.DAL.QueryModels;
using Identity.Exceptions;
using Identity.Mappers;
using Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
public class UserController(IUserBll userBll, ISessionBll sessionBll) : ControllerBase
{
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterQueryModel model)
    {
        UserModel user;
        
        try
        {
            user = await userBll.CreateUserAsync(RegisterQmToUserModel.Map(model));
        }
        catch (ConflictException e)
        {
            return Conflict();
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }

        return Ok(user.Id);
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginQueryModel model)
    {
        try
        {
            if (!await userBll.Authenticate(model.Email, model.Password))
                return Unauthorized();

            var userModelFromDb = await userBll.GetUserByEmailAsync(model.Email);

            var fingerPrint = Request.Headers["FingerPrint"];

            if (string.IsNullOrWhiteSpace(fingerPrint))
                return BadRequest();
            
            var session = await sessionBll.CreateSessionAsync(userModelFromDb.Id, fingerPrint.ToString());
            var jwt = JwtCreator.CreateToken(userModelFromDb.Id, userModelFromDb.Email, userModelFromDb.Role);

            var responce = new
            {
                AccessToken = jwt,
                RefreshToken = session.RefreshToken
            };

            return Ok(responce);
        }
        catch (ConflictException e)
        {
            return Conflict();
        }
        catch (NotFoundException e)
        {
            return NotFound();
        }
    }

    [HttpGet("/refresh")]
    public async Task<IActionResult> Refresh()
    {
        string refreshToken;
        string jwt;
        SessionModel newSession;

        var model = new RefreshQueryModel()
        {
            FingerPrint = Request.Headers["FingerPrint"].ToString(),
            RefreshToken = Request.Headers["RefreshToken"].ToString(),
        };

        try
        {
            newSession = await sessionBll.UpdateSessionAsync(model.RefreshToken, model.FingerPrint);
        }
        catch (NotFoundException e)
        {
            return Unauthorized();
        }
        catch (NotLoggedInException e)
        {
            return Unauthorized();
        }

        var user = await userBll.GetUserByIdAsync(newSession.UserModelId);

        jwt = JwtCreator.CreateToken(user.Id, user.Email, user.Role);

        var response = new
        {
            AccessToken = jwt,
            RefreshToken = newSession.RefreshToken
        };

        return Ok(response);
    }
}