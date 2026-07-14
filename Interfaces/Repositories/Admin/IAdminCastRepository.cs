using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminCastRepository
{
    Task<IEnumerable<EventArtist>> GetCastByPerformance(int performanceId);
    Task AddCast(IEnumerable<EventArtist> cast);
    Task<bool> DeleteCast(int eventId, int artistId);
}