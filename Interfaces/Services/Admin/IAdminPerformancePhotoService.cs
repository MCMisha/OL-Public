using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminPerformancePhotoService
{
    Task<IEnumerable<PerformancePhotoDto>> GetPerformancePhotosByPerformanceIdAsync(int performanceId);
    Task<PerformancePhotoDto?> GetPerformancePhotoByIdAsync(int photoId);
    Task<IEnumerable<PerformancePhoto>> AddPerformancePhotosAsync(int performanceId, CreatePhotosDto dto);
    Task<PerformancePhotoDto?> UpdatePerformancePhotoAsync(int performanceId, int photoId, UpdatePhotoDto dto);
    Task<bool> DeletePerformancePhotoAsync(int performanceId, int photoId);
}