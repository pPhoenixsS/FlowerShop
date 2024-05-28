using Goods.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Goods.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GoodsDb;Username=postgres;Password=111hinata111");
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Images> Images { get; set; }
}