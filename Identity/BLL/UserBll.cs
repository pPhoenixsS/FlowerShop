using Identity.DAL;
using Identity.DAL.Models;
using Identity.Exceptions;
using Identity.Services;

namespace Identity.BLL;

public class UserBll(IUserDal userDal) : IUserBll
{
    public async Task<UserModel> CreateUserAsync(UserModel user)
    {
        var modelFromDb = await userDal.GetUserByEmailAsync(user.Email);
        if (modelFromDb.Id != 0)
            throw new ConflictException();
        user.Salt = Guid.NewGuid().ToString();
        user.Password = Encrypt.HashPassword(user.Password, user.Salt);
        user.Role = Role.User;
        await userDal.CreateUserAsync(user);
        return user;
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        var modelFromDb = await userDal.GetUserByIdAsync(id);
        if (modelFromDb.Id == 0)
            throw new NotFoundException();
        return modelFromDb;
    }

    public async Task<UserModel> GetUserByEmailAsync(string email)
    {
        var modelFromDb = await userDal.GetUserByEmailAsync(email);
        if (modelFromDb.Id == 0)
            throw new NotFoundException();
        return modelFromDb;
    }

    public async Task<string> Login(string email, string password)
    {
        var modelFromDb = await GetUserByEmailAsync(email);
        if (modelFromDb.Password != Encrypt.HashPassword(password, modelFromDb.Salt))
            throw new WrongDataException();
        //TODO доделать логин после логики сессий
        throw new NotImplementedException();
    }
}