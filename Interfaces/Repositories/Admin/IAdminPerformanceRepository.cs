using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminPerformanceRepository : IGenericRepository<Performance>
{
    Task<bool> ExistsPerformanceByGenre(Genre genre);
    Task<bool> ExistsPerformanceByPlace(Place place);
}