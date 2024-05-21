using Goods.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Goods.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=4322;Database=GoodsDb;Username=User;Password=qwe123asd");
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Images> Images { get; set; }
}