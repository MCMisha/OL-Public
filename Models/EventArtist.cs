namespace WebApplicationOperaLublin.Models;

public class EventArtist : Entity
{
    public int EventId { get; set; }
    public int ArtistId { get; set; }

    public string Role { get; set; } = null!;

    public PerformanceEvent PerformanceEventNavigation { get; set; } = null!;
    public Artist Artist { get; set; } = null!;
}