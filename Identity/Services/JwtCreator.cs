using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.DAL.Models;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services;

public static class JwtCreator
{
    public static string CreateToken(int userId, string email, Role role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fK7s92LzN8Xm6T4pGq1YvH5jR3cW8uZb"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("UserId", userId.ToString()),
            new Claim("Email", email),
            new Claim("Role", role.ToString())
        };

        var token = new JwtSecurityToken(
            "IdentityService",
            "FlowerShopServices",
            expires: DateTime.Now.AddHours(0.5),
            signingCredentials: credentials,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}