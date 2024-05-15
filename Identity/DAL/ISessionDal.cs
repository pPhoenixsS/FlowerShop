using Identity.DAL.Models;

namespace Identity.DAL;

public interface ISessionDal
{
    Task<SessionModel> CreateSessionAsync(SessionModel session);
    Task DeleteSessionAsync(int userId);
    Task<SessionModel> GetSessionByUserId(int userId);
}