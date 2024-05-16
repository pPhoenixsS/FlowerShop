using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.QueryModels;

public class LoginQueryModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}