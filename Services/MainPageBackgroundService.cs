using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class MainPageBackgroundService : IMainPageBackgroundService
{
    private readonly IMainPageBackgroundRepository _repository;

    public MainPageBackgroundService(IMainPageBackgroundRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<MainPageBackgroundDto>> GetAllActiveMainPageBackgrounds()
    {
        var backgrounds = await _repository.GetAllActive();
        return backgrounds.Select(background => new MainPageBackgroundDto
        {
            Id = background.Id,
            PerformanceId = background.PerformanceId,
            PerformanceTitle = background.PerformanceNavigation.Title,
            Genre = background.PerformanceNavigation.GenreNavigation.Name,
            MainImage = background.MainImage,

            NearestEvents = background.PerformanceNavigation.PerformanceEvents
                .Where(e => e.IsActive && e.StartAt >= DateTime.UtcNow)
                .OrderBy(e => e.StartAt)
                .Take(2)
                .Select(e => e.StartAt)
                .ToList()
        });;
    }
}