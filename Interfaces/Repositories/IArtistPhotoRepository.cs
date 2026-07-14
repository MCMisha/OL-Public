using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IArtistPhotoRepository
{
    Task<IEnumerable<ArtistPhoto>> GetArtistPhotosByArtistIdAsync(int artistId);
}
