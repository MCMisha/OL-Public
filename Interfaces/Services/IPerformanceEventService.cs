using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IPerformanceEventService
{
    Task<MinMaxDate?> GetMinMaxDate();
    Task<IEnumerable<PerformanceEventDto>> GetEventsByMonth(DateTime start,  DateTime end);
    Task<IEnumerable<PerformanceEventWithImageDto>> GetNeatestSixPerformances();
    Task<IEnumerable<PerformanceEventDto>> GetNearestFiveEventsForPerformanceAsync(int performanceId);
}