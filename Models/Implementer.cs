using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOperaLublin.Models;

[Table("implementer")]
public class Implementer : Entity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public byte[]? Photo { get; set; }
    public bool IsDirector { get; set; }

    public ICollection<PerformanceImplementer> PerformanceImplementers { get; set; } = new List<PerformanceImplementer>();
}