using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminArtistsRepository : IGenericRepository<Artist>
{
    Task<IEnumerable<Artist>> GetCountAsync(int count);
}