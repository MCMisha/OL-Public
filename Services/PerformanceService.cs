using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class PerformanceService : IPerformanceService
{
    private readonly IPerformanceRepository _repository;
    
    public PerformanceService(IPerformanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Performance>> GetAllPerformances()
    {
        return await _repository.GetAllPerformances();
    }

    public async Task<IEnumerable<PerformanceListAboutDto>> GetAllPerformanceListAbout()
    {
        var performances = await _repository.GetAllPerformancesForAbout();
        return performances.Select(p =>
        {
            var firstImplementer = p.PerformanceImplementers.FirstOrDefault()?.Implementer;

            return new PerformanceListAboutDto
            {
                Id = p.Id,
                Title = p.Title,
                MainImage = p.MainImage,
                Genre = p.GenreNavigation.Name,
                BreaksCount = p.BreaksCount,
                DirectorName = firstImplementer == null
                    ? null
                    : $"{firstImplementer.FirstName} {firstImplementer.LastName}",
                PremiereDate = p.PremiereDate,
                NearestEvents = p.PerformanceEvents.Take(5).Where(pe => pe.StartAt > DateTime.Now).Select(pe => pe.StartAt)
            };
        });
    }

    public async Task<Performance?> GetPerformanceById(int id)
    {
        return await _repository.GetPerformanceById(id);
    }

    public async Task<IEnumerable<Performance>> GetNewestPerformancePremieres()
    {
        return await _repository.GetNewestPerformancePremieres();
    }
}