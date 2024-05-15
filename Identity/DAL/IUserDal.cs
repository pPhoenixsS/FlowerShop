using Identity.DAL.Models;

namespace Identity.DAL;

public interface IUserDal
{
    Task<UserModel> CreateUserAsync(UserModel user);
    Task<UserModel> GetUserByIdAsync(int id);
    Task<UserModel> GetUserByEmailAsync(string email);
}