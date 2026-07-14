using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IPerformanceInfoRepository
{
    Task<IEnumerable<Implementer>> GetImplementersByPerformance(int performanceId);
}