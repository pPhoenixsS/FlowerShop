using BonusSystem.DAL.Models;

namespace BonusSystem.DAL;

public class BonusDal : IBonusDal
{
    public async Task<Bonus> CreateAsync(Bonus bonus)
    {
        await using var db = new DbHelper();
        await db.Bonuses.AddAsync(bonus);
        await db.SaveChangesAsync();
        return bonus;
    }

    public async Task<Bonus> GetBonusAsync(int userId)
    {
        await using var db = new DbHelper();
        var bonus = await db.Bonuses.FindAsync(userId) ?? new Bonus();
        return bonus;
    }

    public async Task<Bonus> UpdateBonuses(Bonus bonus)
    {
        await using var db = new DbHelper();
        db.Bonuses.Update(bonus);
        await db.SaveChangesAsync();
        return bonus;
    }
}