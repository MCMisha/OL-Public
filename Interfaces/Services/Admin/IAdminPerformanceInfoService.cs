using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminPerformanceInfoService
{
    Task<IEnumerable<Implementer>> AddImplementers(IEnumerable<ImplementerCreateUpdateDto> implementers, int performanceId);
    Task<IEnumerable<ImplementerCreateUpdateDto>> GetImplementersByPerformance(int performanceId);
    Task<ImplementerCreateUpdateDto?> GetImplementerById(int performanceId, int implementerId);
    Task<Implementer> UpdateImplementer(int performanceId, int implementerId, ImplementerCreateUpdateDto implementer);
    Task<bool> DeleteImplementer(int performanceId, int implementerId);
}