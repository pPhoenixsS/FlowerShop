using Identity.DAL.Models;

namespace Identity.Services;

public static class BonusSystem
{
    public static async Task Register(int userId, string email)
    {
        using var client = new HttpClient();
        var token = JwtCreator.CreateToken(userId, email, Role.User);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var qwe = await client.PostAsync("http://localhost:5003/bonus", new MultipartContent());
        var asd = qwe.IsSuccessStatusCode;
        
    }
}