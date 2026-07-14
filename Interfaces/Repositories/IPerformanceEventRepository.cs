using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IPerformanceEventRepository
{
    // Task<PerformanceEvent?> GetEventByIdAsync(int id);
    Task<MinMaxDate?> GetMinMaxDateAsync();
    Task<IEnumerable<PerformanceEventDto>> GetPerformanceEventsByMonthAsync(DateTime start,  DateTime end);
    Task<IEnumerable<PerformanceEvent>> GetNeatestSixPerformancesAsync();
    Task<IEnumerable<PerformanceEventDto>> GetNearestFiveEventsForPerformanceAsync(int performanceId);
}