namespace WebApplicationOperaLublin.Models.DTO;

public class CastCreateDto
{
    public int EventId { get; set; }
    public int ArtistId { get; set; }
    public required string Role { get; set; }
}