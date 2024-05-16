using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.QueryModels;

public class RefreshQueryModel
{
    [Required]
    public string RefreshToken { get; set; } = null!;
    [Required]
    public string FingerPrint { get; set; } = null!;
}