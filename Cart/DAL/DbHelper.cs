using Cart.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=4323;Database=CartDb;Username=User;Password=qwe123asd");
    }
    
    public DbSet<CartModel> Carts { get; set; }
}