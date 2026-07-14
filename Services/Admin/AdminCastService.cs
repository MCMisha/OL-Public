using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminCastService : IAdminCastService
{
    private readonly IAdminCastRepository _repository;
    private readonly IAdminPerformanceRepository _performanceRepository;

    public AdminCastService(IAdminCastRepository repository, IAdminPerformanceRepository performanceRepository)
    {
        _repository = repository;
        _performanceRepository = performanceRepository;
    }

    public async Task<IEnumerable<ArtistCastDto>> GetCast(int performanceId)
    {
        var cast = await _repository.GetCastByPerformance(performanceId);
        
        return cast.Select(eventArtist => new ArtistCastDto
        {
            Id = eventArtist.Id,
            ArtistId = eventArtist.ArtistId,
            EventId = eventArtist.EventId,
            Role = eventArtist.Role
        });;
    }

    public async Task AddCast(IEnumerable<CastCreateDto> artistIds)
    {
        var cast = artistIds.Select(cvm => new EventArtist
        {
            EventId = cvm.EventId,
            ArtistId = cvm.ArtistId,
            Role = cvm.Role
        });

        await _repository.AddCast(cast);
    }

    public async Task<bool> RemoveCast(int performanceId, int eventId, int artistId)
    {
        var performance = await _performanceRepository.GetByIdAsync(performanceId);
        if (performance == null)
        {
            throw new PerformanceNotFoundException(performanceId);
        }

        return await _repository.DeleteCast(eventId, artistId);
    }
}