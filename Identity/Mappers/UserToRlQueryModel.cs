using Identity.DAL.Models;
using Identity.DAL.QueryModels;

namespace Identity.Mappers;

public static class RlQueryToUserModel
{
    public static UserModel Map(RegisterLoginQueryModel queryModel)
    {
        var userModel = new UserModel()
        {
            Email = queryModel.Email,
            Password = queryModel.Password
        };

        return userModel;
    }
}