using System.Text;
using Newtonsoft.Json;

namespace FlowerShop.Tests;

public class UnitTest1
{
    class Tokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    class Cart
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
    private string email = "qwe@qwe.cj";
    private string password = "qweasd";
    private string AccessToken { get; set; }
    private string RefreshToken { get; set; }
    
    [Fact]
    public async void TestRegistration()
    {
        Thread thread = new Thread(async () =>
        {
            var body = new
            {
                email = email,
                password = password
            };
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("FingerPrint", "qweasd");
            var responce = await client.PostAsync("http://localhost:7000/register",
                content);
            Assert.True(responce.IsSuccessStatusCode);
            responce = await client.PostAsync("http://localhost:7000/register",
                content);
            Assert.False(responce.IsSuccessStatusCode);
        });
        thread.Start();
        thread.Join();
    }
    
    [Fact]
    public async void TestLogin()
    {
        await Task.Delay(5000);
        var body = new
        {
            email = email,
            password = password
        };
        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("FingerPrint", "qweasd");
        var responce = await client.PostAsync("http://localhost:7000/login",
            content);
        var responseContent = JsonConvert.DeserializeObject<Tokens>(await responce.Content.ReadAsStringAsync());
        AccessToken = responseContent.AccessToken;
        RefreshToken = responseContent.RefreshToken;
        Assert.True(responce.IsSuccessStatusCode);
        Assert.False(string.IsNullOrWhiteSpace(AccessToken));
        Assert.False(string.IsNullOrWhiteSpace(RefreshToken));
    }

    [Fact]
    public async void TestCart()
    {
        TestLogin();
        var body = new
        {
            ProductId = 1,
            Count = 5
        };
        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", AccessToken);
        var responce = await client.PostAsync("http://localhost:7000/cart",
            content);
        var responseContent = JsonConvert.DeserializeObject<List<Cart>>(await responce.Content.ReadAsStringAsync());
        Assert.True(responce.IsSuccessStatusCode);
        Assert.True(responseContent.Count==1);
        Assert.True(responseContent[0].ProductId==1);
        Assert.True(responseContent[0].UserId==2);
    }
}