using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IArtistPhotoService
{
    Task<IEnumerable<ArtistPhotoDto>> GetArtistPhotosByArtistIdAsync(int artistId);
}