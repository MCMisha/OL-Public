namespace WebApplicationOperaLublin.Models.DTO;

public class ArtistPhotoDto
{
    public int Id { get; set; }

    public int ArtistId { get; set; }

    public string Photo { get; set; } = string.Empty;
}