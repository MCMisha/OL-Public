using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminPerformanceEventService
{
    Task<IEnumerable<AdminPerformanceEventListDto>> GetAllEvents();
    Task<IEnumerable<AdminPerformanceEventListDto>> GetByPerformanceId(int performanceId);
    Task<PerformanceEventDto?> GetEventById(int id);
    Task<AdminPerformanceEventListDto?> CreateEvent(PerformanceEventCreateUpdateDto newEvent);
    Task<PerformanceEventUpdateDto?> UpdateEvent(PerformanceEventCreateUpdateDto updatedEvent);
    Task UpdateEventsForPerformance(int performanceId, IEnumerable<AdminPerformanceEventUpdateDto> newEvents);
    Task<bool> DeleteEvent(int id);
    
}