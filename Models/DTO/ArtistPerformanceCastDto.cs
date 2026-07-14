using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Models.DTO;

public class ArtistPerformanceCastDto
{
    public int Id { get; set; }
    public int ArtistId { get; set; }
    public DateTime StartAt { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public byte[]? Photo { get; set; }
    public required string Role { get; set; }
    public ArtistCategory Category { get; set; }
}