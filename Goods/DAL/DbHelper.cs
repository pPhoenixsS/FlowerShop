using Identity.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=4321;Database=GoodsDb;Username=User;Password=qwe123asd");
    }
    
    public DbSet<Product> Products { get; set; }
}