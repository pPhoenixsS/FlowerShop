using Identity.DAL.Models;

namespace Identity.BLL;

public interface ISessionBll
{
    Task<SessionModel> UpdateSessionAsync(string refreshToken, string fingerPrint);
    Task DeleteSessionAsync(string refreshToken, string fingerPrint);
    Task<SessionModel> CreateSessionAsync(int userId, string fingerPrint);
}