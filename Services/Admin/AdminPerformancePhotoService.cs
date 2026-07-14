using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminPerformancePhotoService : IAdminPerformancePhotoService
{
    private readonly IAdminPerformancePhotoRepository  _repository;

    public AdminPerformancePhotoService(IAdminPerformancePhotoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PerformancePhotoDto>> GetPerformancePhotosByPerformanceIdAsync(int performanceId)
    {
        var photos = await _repository.GetPerformancePhotosByPerformanceId(performanceId);
        
        return photos.Select(MapToDto);
    }

    public async Task<PerformancePhotoDto?> GetPerformancePhotoByIdAsync(int photoId)
    {
        var photo = await _repository.GetByIdAsync(photoId);

        if (photo == null)
        {
            return null;
        }

        return MapToDto(photo);
    }

    public async Task<IEnumerable<PerformancePhoto>?> AddPerformancePhotosAsync(int performanceId, CreatePhotosDto dto)
    {
        if (dto.Photos == null || dto.Photos.Count == 0)
        {
            return null;
        }
        
        var performancePhotos = new List<PerformancePhoto>();

        foreach (var file in dto.Photos)
        {
            if (file == null || file.Length == 0)
            {
                continue;
            }

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            performancePhotos.Add(new PerformancePhoto
            {
                PerformanceId = performanceId,
                Photo = memoryStream.ToArray()
            });
        }

        if (!performancePhotos.Any())
        {
            return null;
        }
        
        await _repository.AddPerformancePhotosAsync(performancePhotos);
        return performancePhotos;
    }
    
    public async Task<PerformancePhotoDto?> UpdatePerformancePhotoAsync(int performanceId, int photoId, UpdatePhotoDto dto)
    {
        if (dto.Photo == null || dto.Photo.Length == 0)
        {
            return null;
        }

        var existingPhoto = await _repository.GetByIdAsync(photoId);

        if (existingPhoto == null || existingPhoto.PerformanceId != performanceId)
        {
            return null;
        }

        await using var memoryStream = new MemoryStream();
        await dto.Photo.CopyToAsync(memoryStream);

        existingPhoto.Photo = memoryStream.ToArray();

        await _repository.UpdateAsync(existingPhoto);

        return MapToDto(existingPhoto);
    }

    public async Task<bool> DeletePerformancePhotoAsync(int performanceId, int photoId)
    {
        var existingPhoto = await _repository.GetByIdAsync(photoId);

        if (existingPhoto == null || existingPhoto.PerformanceId != performanceId)
        {
            return false;
        }

        await _repository.DeleteAsync(photoId);

        return true;
    }

    private PerformancePhotoDto MapToDto(PerformancePhoto photo)
    {
        return new PerformancePhotoDto
        {
            Id = photo.Id,
            PerformanceId = photo.PerformanceId,
            Photo = Convert.ToBase64String(photo.Photo)
        };
    }
}