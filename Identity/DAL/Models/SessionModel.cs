namespace Identity.DAL.Models;

public class SessionModel
{
    public int Id { get; set; }
    public int UserModelId { get; set; }
    public string RefreshToken { get; set; } = null!;
    public string FingerPrint { get; set; } = null!;
    public int ExpiresIn { get; set; } = 60;
    public DateTime CreatedAt { get; set; }
}