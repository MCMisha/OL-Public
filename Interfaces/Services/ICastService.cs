using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface ICastService
{
    Task<IEnumerable<ArtistPerformanceCastDto>> GetCast(int performanceId);
}