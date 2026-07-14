using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminPerformanceInfoRepository
{
    Task<IEnumerable<Implementer>> AddImplementers(IEnumerable<Implementer> implementers, int performanceId);
    Task<IEnumerable<Implementer>> GetImplementersByPerformance(int performanceId);
    Task<Implementer?> GetImplementerById(int performanceId, int implementerId);
    Task<Implementer> UpdateImplementer(int performanceId, int implementerId, Implementer implementer);
    Task<bool> DeleteImplementer(int performanceId, int implementerId);
}