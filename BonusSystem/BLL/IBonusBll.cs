using BonusSystem.DAL.Models;

namespace BonusSystem.BLL;

public interface IBonusBll
{
    Task<Bonus> CreateAsync(int userId);
    Task<Bonus> GetBonusAsync(int userId);
    Task<Bonus> UpdateBonuses(int userId, int count);
}