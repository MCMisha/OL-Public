namespace WebApplicationOperaLublin.Models;

public class PerformancePhoto : Entity
{
    public int PerformanceId { get; set; }
    public required byte[] Photo { get; set; }
    public virtual Performance PerformanceNavigation { get; set; } = null!;
}