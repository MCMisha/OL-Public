using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface ICastRepository
{
    Task<IEnumerable<EventArtist>> GetCastByPerformance(int performanceId);
}