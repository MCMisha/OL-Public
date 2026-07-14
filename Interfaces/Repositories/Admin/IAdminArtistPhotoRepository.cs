using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminArtistPhotoRepository : IGenericRepository<ArtistPhoto>
{
    Task<IEnumerable<ArtistPhoto>> GetArtistPhotosByArtistIdAsync(int artistId);
    Task AddPhotosAsync(IEnumerable<ArtistPhoto> artistPhotos);
    Task DeletePhotosByArtistIdAsync(int artistId);
}