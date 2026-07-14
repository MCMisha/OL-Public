using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class PerformancePhotoService : IPerformancePhotoService
{
    private readonly IPerformancePhotoRepository _repository;
    public PerformancePhotoService(IPerformancePhotoRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<PerformancePhotoDto>> GetPerformancePhotosByPerformanceIdAsync(int performanceId)
    {
        var photos = await _repository.GetPerformancePhotosByPerformanceId(performanceId);
        
        return photos.Select(MapToDto);
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