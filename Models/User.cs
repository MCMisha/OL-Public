using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOperaLublin.Models;

[Table("user")]
public class User : Entity
{
    [Required] public string Login { get; set; } = null!;
    [Required] public string PasswordHash { get; set; } = null!;
    [Required] public int LoginAttempt { get; set; } = 0;
    public DateTime? LastLoginAttemptTime { get; set; } = null;
    public string? RefreshTokenHash { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }
}