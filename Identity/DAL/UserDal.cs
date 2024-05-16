using Identity.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL;

public class UserDal : IUserDal
{
    public async Task<UserModel> CreateUserAsync(UserModel user)
    {
        await using var db = new DbHelper();
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        return user;
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        await using var db = new DbHelper();
        var modelFromDb = await db.Users.FindAsync(id) ?? new UserModel();
        return modelFromDb;
    }

    public async Task<UserModel> GetUserByEmailAsync(string email)
    {
        await using var db = new DbHelper();
        var modelFromDb = await db.Users.FirstOrDefaultAsync(u => u.Email == email) ?? new UserModel();
        return modelFromDb;

    }
}