using Newtonsoft.Json;

namespace Goods.Services;

public static class BonusSystem
{
    public static async Task UpdateBonuses(int bonuses, string token, double cost)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", token);

        var responce = await client.GetAsync("http://localhost:5003/bonus");

        var content = await responce.Content.ReadAsStringAsync();
        var currentBonuses = JsonConvert.DeserializeObject<Bonus>(content);

        if (bonuses > currentBonuses.Bonuses)
            throw new Exception();

        int newBonuses = 0;
        if (bonuses == 0)
        {
            newBonuses = currentBonuses.Bonuses + (int)(cost * 0.07);
            await client.PutAsync($"http://localhost:5003/bonus/{newBonuses.ToString()}", new MultipartContent());
            return;
        }
        
        newBonuses = currentBonuses.Bonuses - bonuses;
        await client.PutAsync($"http://localhost:5003/bonus/{newBonuses.ToString()}", new MultipartContent());
    }

    class Bonus
    {
        public int UserId { get; set; }
        public int Bonuses { get; set; }
    }
}