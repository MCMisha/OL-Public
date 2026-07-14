using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Models.DTO;

public class ArtistWithPhotosAndPerformancesDto
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required ArtistCategory Category { get; set; }
    public byte[]? Photo { get; set; }
    public required string Description { get; set; }
    public required IEnumerable<byte[]> ArtistPhotos { get; set; }
    public required IEnumerable<PerformanceEventWithImageDto>  ArtistEvents { get; set; }
}