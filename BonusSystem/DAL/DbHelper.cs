using BonusSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BonusSystem.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BonusDb;Username=postgres;Password=111hinata111");
    }
    
    public DbSet<Bonus> Bonuses { get; set; }
}