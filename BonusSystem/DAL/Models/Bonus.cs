using System.ComponentModel.DataAnnotations;

namespace BonusSystem.DAL.Models;

public class Bonus
{
    [Key]
    public int UserId { get; set; }
    public int Bonuses { get; set; }
}