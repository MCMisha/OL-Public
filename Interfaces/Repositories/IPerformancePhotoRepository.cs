using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IPerformancePhotoRepository
{
    Task<IEnumerable<PerformancePhoto>> GetPerformancePhotosByPerformanceId(int performanceId);
}