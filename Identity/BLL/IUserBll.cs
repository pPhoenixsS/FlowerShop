using Identity.DAL.Models;

namespace Identity.BLL;

public interface IUserBll
{
    Task<UserModel> CreateUserAsync(UserModel user);
    Task<UserModel> GetUserByIdAsync(int id);
    Task<UserModel> GetUserByEmailAsync(string email);
    Task<string> Login(string email, string password);
}