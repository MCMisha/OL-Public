using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IPerformanceInfoService
{
    Task<IEnumerable<ImplementerCreateUpdateDto>> GetImplementersByPerformance(int performanceId);
}