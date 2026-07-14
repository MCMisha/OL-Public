using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Models;

[Table("artist")]
public class Artist : Entity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public byte[]? Photo { get; set; }
    public string? Description { get; set; }
    
    public ArtistCategory Category { get; set; }
    
    public ICollection<EventArtist> EventArtists { get; set; } = new List<EventArtist>();

    public ICollection<ArtistPhoto> Photos { get; set; } = new List<ArtistPhoto>();
}
