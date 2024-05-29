using Identity.DAL;
using Identity.DAL.Models;
using Identity.Exceptions;

namespace Identity.BLL;

public class SessionBll(ISessionDal sessionDal) : ISessionBll
{
    public async Task<SessionModel> UpdateSessionAsync(string refreshToken, string fingerPrint)
    {
        var sessionFromDb = await sessionDal.GetSessionByToken(refreshToken, fingerPrint);
        if (sessionFromDb.Id == 0)
            throw new NotFoundException();
        if (DateTime.Now > sessionFromDb.CreatedAt + TimeSpan.FromDays(sessionFromDb.ExpiresIn))
        {
            await sessionDal.DeleteSessionAsync(refreshToken, fingerPrint);
            throw new NotLoggedInException();
        }

        sessionFromDb.RefreshToken = Guid.NewGuid().ToString();
        await sessionDal.UpdateSessionAsync(sessionFromDb);

        return sessionFromDb;
    }

    public async Task DeleteSessionAsync(string refreshToken, string fingerPrint)
    {
        await sessionDal.DeleteSessionAsync(refreshToken, fingerPrint);
    }

    public async Task<SessionModel> CreateSessionAsync(int userId, string fingerPrint)
    {
        var sessionFromDb = await sessionDal.GetSessionByFingerPrintAsync(fingerPrint);

        if (sessionFromDb.Id != 0)
        {
            var oldSession = new SessionModel()
            {
                Id = sessionFromDb.Id,
                UserModelId = userId,
                RefreshToken = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                FingerPrint = fingerPrint
            };

            await sessionDal.UpdateSessionAsync(oldSession);
            
            return oldSession;
        }
        
        var session = new SessionModel()
        {
            UserModelId = userId,
            RefreshToken = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            FingerPrint = fingerPrint
        };

        await sessionDal.CreateSessionAsync(session);
        return session;
    }
}