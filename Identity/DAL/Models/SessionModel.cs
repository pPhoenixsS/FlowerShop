namespace Identity.DAL.Models;

public class SessionModel
{
    public int Id { get; set; }
    public int UserModelId { get; set; }
    public string RefreshToken { get; set; } = null!;
    public long ExpiresIn { get; set; }
    public DateTime CreatedAt { get; set; }
}