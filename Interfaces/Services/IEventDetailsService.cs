using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IEventDetailsService
{
    Task<IEnumerable<EventDateInfo>> GetDatesForPerformanceAsync(int id);
    Task<IEnumerable<EventInstanceInfo>> GetEventDatesAsync(string yearMonth);
    Task<MinMaxDate?> GetMinMaxDateAsync();
}