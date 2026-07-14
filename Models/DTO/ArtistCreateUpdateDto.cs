namespace WebApplicationOperaLublin.Models.DTO;

public class ArtistCreateUpdateDto : Entity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Photo { get; set; }
    public string? Description { get; set; }
    public int Category { get; set; }
}