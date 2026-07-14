namespace WebApplicationOperaLublin.Models;

public class ArtistPhoto : Entity
{
    public int ArtistId { get; set; }
    public byte[] Photo { get; set; } = null!;
    public virtual Artist ArtistNavigation { get; set; } = null!;
}