using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOperaLublin.Models;

[Table("performance_implementer")]
public class PerformanceImplementer : Entity
{
    public int PerformanceId { get; set; }
    public int ImplementerId { get; set; }

    public Performance Performance { get; set; } = null!;
    public Implementer Implementer { get; set; } = null!;
}