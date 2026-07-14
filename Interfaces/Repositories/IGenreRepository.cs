using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetGenres();
    Task<Genre?> GetGenreById(int genreId);
    Task<IEnumerable<Genre>> GetGenresByIds(List<int> ids);
}