using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminCastService
{
    Task<IEnumerable<ArtistCastDto>> GetCast(int performanceId);
    Task AddCast(IEnumerable<CastCreateDto> artistIds);
    Task<bool> RemoveCast(int performanceId, int eventId, int artistId);
}