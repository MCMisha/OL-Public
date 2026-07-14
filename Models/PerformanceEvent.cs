namespace WebApplicationOperaLublin.Models;

public class PerformanceEvent : Entity
{
    public int PerformanceId { get; set; }

    public DateTime StartAt { get; set; }
    
    public string? BuyLink { get; set; }

    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Performance Performance { get; set; } = null!;
    
    public ICollection<EventArtist> EventArtists { get; set; } = new List<EventArtist>();
}