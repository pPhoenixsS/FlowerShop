using Identity.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL;

public class SessionDal : ISessionDal
{
    public async Task<SessionModel> CreateSessionAsync(SessionModel session)
    {
        await using var db = new DbHelper();
        await db.Sessions.AddAsync(session);
        await db.SaveChangesAsync();
        return session;
    }

    public async Task DeleteSessionAsync(string refreshToken, string fingerPrint)
    {
        await Task.Run(async () =>
        {
            await using var db = new DbHelper();
            var sessionFromDb = await GetSessionByToken(refreshToken, fingerPrint);
            db.Remove(sessionFromDb);
            await db.SaveChangesAsync();
        });
    }

    public async Task<SessionModel> GetSessionByFingerPrintAsync(string fingerPrint)
    {
        await using var db = new DbHelper();
        var sessionFromDb = await db.Sessions
            .FirstOrDefaultAsync(s => s.FingerPrint == fingerPrint) ?? new SessionModel();
        return sessionFromDb;
    }

    public async Task<SessionModel> GetSessionByToken(string token, string fingerPrint)
    {
        await using var db = new DbHelper();
        var sessionFromDb = await db.Sessions
            .FirstOrDefaultAsync(s => s.RefreshToken == token 
                                      && s.FingerPrint == fingerPrint) ?? new SessionModel();
        return sessionFromDb;
    }

    public async Task<SessionModel> UpdateSessionAsync(SessionModel session)
    {
        await using var db = new DbHelper();
        db.Sessions.Update(session);
        await db.SaveChangesAsync();
        return session;
    }
}