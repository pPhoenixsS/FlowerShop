using Identity.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL;

public class DbHelper : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Identity;Username=postgres;Password=111hinata111");
    }
    
    public DbSet<UserModel> Users { get; set; }
    public DbSet<SessionModel> Sessions { get; set; }
}