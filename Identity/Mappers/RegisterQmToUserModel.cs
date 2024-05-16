using Identity.DAL.Models;
using Identity.DAL.QueryModels;

namespace Identity.Mappers;

public static class RegisterQmToUserModel
{
    public static UserModel Map(RegisterQueryModel queryModel)
    {
        var userModel = new UserModel()
        {
            Email = queryModel.Email,
            Password = queryModel.Password
        };

        return userModel;
    }
}