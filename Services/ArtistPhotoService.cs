using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class ArtistPhotoService : IArtistPhotoService
{
    private readonly IArtistPhotoRepository _artistPhotoRepository;

    public ArtistPhotoService(IArtistPhotoRepository artistPhotoRepository)
    {
        _artistPhotoRepository = artistPhotoRepository;
    }
    
    public async Task<IEnumerable<ArtistPhotoDto>> GetArtistPhotosByArtistIdAsync(int artistId)
    {
        var photos = await _artistPhotoRepository.GetArtistPhotosByArtistIdAsync(artistId);

        return photos.Select(MapToDto);
    }
    
    private static ArtistPhotoDto MapToDto(ArtistPhoto artistPhoto)
    {
        return new ArtistPhotoDto
        {
            Id = artistPhoto.Id,
            ArtistId = artistPhoto.ArtistId,
            Photo = Convert.ToBase64String(artistPhoto.Photo)
        };
    }
}