using BonusSystem.DAL;
using BonusSystem.DAL.Models;

namespace BonusSystem.BLL;

public class BonusBll(IBonusDal bonusDal) : IBonusBll
{
    public async Task<Bonus> CreateAsync(int userId)
    {
        var bonus = new Bonus() { UserId = userId };
        await bonusDal.CreateAsync(bonus);
        return bonus;
    }

    public async Task<Bonus> GetBonusAsync(int userId)
    {
        var bonus = await bonusDal.GetBonusAsync(userId);
        if (bonus.UserId == 0)
        {
            return await CreateAsync(userId);
        }

        return bonus;
    }

    public async Task<Bonus> UpdateBonuses(int userId, int count)
    {
        var bonus = new Bonus() { UserId = userId, Bonuses = count };
        await bonusDal.UpdateBonuses(bonus);
        return bonus;
    }
}