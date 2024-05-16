namespace Identity.DAL.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public Role Role { get; set; }
}