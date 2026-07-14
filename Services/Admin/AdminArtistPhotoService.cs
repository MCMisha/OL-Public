using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminArtistPhotoService : IAdminArtistPhotoService
{
    private readonly IAdminArtistPhotoRepository _artistPhotoRepository;

    public AdminArtistPhotoService(IAdminArtistPhotoRepository artistPhotoRepository)
    {
        _artistPhotoRepository = artistPhotoRepository;
    }

    public async Task<IEnumerable<ArtistPhotoDto>> GetArtistPhotosByArtistIdAsync(int artistId)
    {
        var photos = await _artistPhotoRepository.GetArtistPhotosByArtistIdAsync(artistId);

        return photos.Select(MapToDto);
    }

    public async Task<ArtistPhotoDto?> GetArtistPhotoByIdAsync(int photoId)
    {
        var photo = await _artistPhotoRepository.GetByIdAsync(photoId);

        if (photo == null)
        {
            return null;
        }

        return MapToDto(photo);
    }

    public async Task<IEnumerable<ArtistPhotoDto>?> AddPhotosAsync(int artistId, CreatePhotosDto dto)
    {
        if (dto.Photos == null || dto.Photos.Count == 0)
        {
            return null;
        }

        var artistPhotos = new List<ArtistPhoto>();

        foreach (var file in dto.Photos)
        {
            if (file == null || file.Length == 0)
            {
                continue;
            }

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            artistPhotos.Add(new ArtistPhoto
            {
                ArtistId = artistId,
                Photo = memoryStream.ToArray()
            });
        }

        if (!artistPhotos.Any())
        {
            return null;
        }

        await _artistPhotoRepository.AddPhotosAsync(artistPhotos);

        return artistPhotos.Select(MapToDto);
    }

    public async Task<ArtistPhotoDto?> UpdatePhotoAsync(int artistId, int photoId, UpdatePhotoDto dto)
    {
        if (dto.Photo == null || dto.Photo.Length == 0)
        {
            return null;
        }

        var existingPhoto = await _artistPhotoRepository.GetByIdAsync(photoId);

        if (existingPhoto == null || existingPhoto.ArtistId != artistId)
        {
            return null;
        }

        await using var memoryStream = new MemoryStream();
        await dto.Photo.CopyToAsync(memoryStream);

        existingPhoto.Photo = memoryStream.ToArray();

        await _artistPhotoRepository.UpdateAsync(existingPhoto);

        return MapToDto(existingPhoto);
    }

    public async Task<bool> DeletePhotoAsync(int artistId, int photoId)
    {
        var existingPhoto = await _artistPhotoRepository.GetByIdAsync(photoId);

        if (existingPhoto == null || existingPhoto.ArtistId != artistId)
        {
            return false;
        }

        await _artistPhotoRepository.DeleteAsync(photoId);

        return true;
    }

    private ArtistPhotoDto MapToDto(ArtistPhoto artistPhoto)
    {
        return new ArtistPhotoDto
        {
            Id = artistPhoto.Id,
            ArtistId = artistPhoto.ArtistId,
            Photo = Convert.ToBase64String(artistPhoto.Photo)
        };
    }
}