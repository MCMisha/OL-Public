using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IArtistsRepository
{
    Task<IEnumerable<Artist>> GetCountAsync(int count);
    Task<Artist?> GetByIdAsync(int id);
    Task<IEnumerable<Artist>> GetAllAsync();
}