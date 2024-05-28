using BonusSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BonusSystem.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=4324;Database=BonusDb;Username=User;Password=qwe123asd");
    }
    
    public DbSet<Bonus> Bonuses { get; set; }
}