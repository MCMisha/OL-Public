namespace WebApplicationOperaLublin.Models;

public class MainPageBackground : Entity
{
    public int PerformanceId { get; set; }

    public byte[] MainImage { get; set; } = null!;  
    
    public bool IsActive { get; set; } = true;

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual Performance PerformanceNavigation { get; set; } = null!;
}