using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminPerformanceEventRepository : IGenericRepository<PerformanceEvent>
{
    Task<IEnumerable<AdminPerformanceEventListDto>> GetAllPerformanceEventsAsync();
    Task<IEnumerable<AdminPerformanceEventListDto>> GetByPerformanceId(int performanceId);
    Task<PerformanceEvent?> GetEventByIdAsync(int id);
    Task<AdminPerformanceEventListDto?> CreateEventAsync(PerformanceEvent entity);
    Task<PerformanceEventUpdateDto?> UpdateEventAsync(PerformanceEvent entity);
    Task UpdatePerformanceEventsForPerformance(int performanceId, List<PerformanceEvent> events);
}