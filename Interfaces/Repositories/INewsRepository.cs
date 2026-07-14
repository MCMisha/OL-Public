using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface INewsRepository : IGenericRepository<News>
{
    Task<IEnumerable<News>> GetFiveRecent();
}