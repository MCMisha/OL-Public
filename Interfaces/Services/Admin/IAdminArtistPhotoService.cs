using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminArtistPhotoService
{
    Task<IEnumerable<ArtistPhotoDto>> GetArtistPhotosByArtistIdAsync(int artistId);

    Task<ArtistPhotoDto?> GetArtistPhotoByIdAsync(int photoId);

    Task<IEnumerable<ArtistPhotoDto>?> AddPhotosAsync(int artistId, CreatePhotosDto dto);

    Task<ArtistPhotoDto?> UpdatePhotoAsync(int artistId, int photoId, UpdatePhotoDto dto);

    Task<bool> DeletePhotoAsync(int artistId, int photoId);
}