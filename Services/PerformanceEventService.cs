using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class PerformanceEventService : IPerformanceEventService
{
    private readonly IPerformanceEventRepository _repository;

    public PerformanceEventService(IPerformanceEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<MinMaxDate?> GetMinMaxDate()
    {
        return await _repository.GetMinMaxDateAsync();
    }
    
    public async Task<IEnumerable<PerformanceEventDto>> GetEventsByMonth(DateTime start,  DateTime end)
    {
        return await _repository.GetPerformanceEventsByMonthAsync(start, end);
    }

    public async Task<IEnumerable<PerformanceEventWithImageDto>> GetNeatestSixPerformances()
    {
        var neatestSixPerformances = await _repository.GetNeatestSixPerformancesAsync();
        return neatestSixPerformances.Select(pe => new PerformanceEventWithImageDto
            {
                EventId = pe.Id,
                StartAt = pe.StartAt,
                PerformanceId = pe.PerformanceId,
                Title = pe.Performance.Title,
                Genre = pe.Performance.GenreNavigation.Name,
                Place = pe.Performance.PlaceNavigation.Name,
                MainImage = pe.Performance.MainImage,
                BuyLink = pe.BuyLink ?? string.Empty,
                IsActive = pe.IsActive
            })
            .Take(6);
    }

    public async Task<IEnumerable<PerformanceEventDto>> GetNearestFiveEventsForPerformanceAsync(int performanceId)
    {
        return await _repository.GetNearestFiveEventsForPerformanceAsync(performanceId);
    }
}