using Identity.DAL.Models;

namespace Identity.DAL;

public interface ISessionDal
{
    Task<SessionModel> CreateSessionAsync(SessionModel session);
    Task DeleteSessionAsync(string refreshToken, string fingerPrint);
    Task<SessionModel> GetSessionByFingerPrintAsync(string fingerPrint);
    Task<SessionModel> GetSessionByToken(string token, string fingerPrint);
    Task<SessionModel> UpdateSessionAsync(SessionModel session);
}