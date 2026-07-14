using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminPerformancePhotoRepository : IGenericRepository<PerformancePhoto>
{
    Task<IEnumerable<PerformancePhoto>> GetPerformancePhotosByPerformanceId(int performanceId);
    Task AddPerformancePhotosAsync(IEnumerable<PerformancePhoto> performancePhotos);
    Task DeletePerformancePhotosAsync(int performanceId);
}