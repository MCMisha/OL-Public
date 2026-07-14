using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _repository;

    public CastService(ICastRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ArtistPerformanceCastDto>> GetCast(int performanceId)
    {
        var cast = await _repository.GetCastByPerformance(performanceId);
        
        return cast.Select(eventArtist => new ArtistPerformanceCastDto()
        {
            Id = eventArtist.Id,
            ArtistId = eventArtist.ArtistId,
            StartAt = eventArtist.PerformanceEventNavigation.StartAt,
            FirstName = eventArtist.Artist.FirstName,
            LastName = eventArtist.Artist.LastName,
            Photo = eventArtist.Artist.Photo,
            Role = eventArtist.Role,
            Category = eventArtist.Artist.Category
        });
    }
}