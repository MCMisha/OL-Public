using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IPerformancePhotoService
{
    Task<IEnumerable<PerformancePhotoDto>> GetPerformancePhotosByPerformanceIdAsync(int performanceId);
}