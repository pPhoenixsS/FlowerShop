using Identity.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=4321;Database=IdentityDb;Username=User;Password=qwe123asd");
    }
    
    public DbSet<UserModel> Users { get; set; }
    public DbSet<SessionModel> Sessions { get; set; }
}