using Identity.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL;

public class SessionDal : ISessionDal
{
    public async Task<SessionModel> CreateSessionAsync(SessionModel session)
    {
        await using var db = new DbHelper();
        await db.Sessions.AddAsync(session);
        return session;
    }

    public async Task DeleteSessionAsync(int userId)
    {
        await Task.Run(async () =>
        {
            await using var db = new DbHelper();
            var sessionFromDb = await GetSessionByUserId(userId);
            db.Remove(sessionFromDb);
        });
    }

    public async Task<SessionModel> GetSessionByUserId(int userId)
    {
        await using var db = new DbHelper();
        var sessionFromDb = await db.Sessions
            .FirstOrDefaultAsync(s => s.UserModelId == userId) ?? new SessionModel();
        return sessionFromDb;
    }
}