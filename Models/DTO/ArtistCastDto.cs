namespace WebApplicationOperaLublin.Models.DTO;

public class ArtistCastDto
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int ArtistId { get; set; }
    public required string Role { get; set; }
}