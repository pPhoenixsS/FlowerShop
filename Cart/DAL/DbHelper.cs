using Cart.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CartDb;Username=postgres;Password=111hinata111");
    }
    
    public DbSet<CartModel> Carts { get; set; }
}