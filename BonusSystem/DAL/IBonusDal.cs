using BonusSystem.DAL.Models;

namespace BonusSystem.DAL;

public interface IBonusDal
{
    Task<Bonus> CreateAsync(Bonus bonus);
    Task<Bonus> GetBonusAsync(int userId);
    Task<Bonus> UpdateBonuses(Bonus bonus);
}