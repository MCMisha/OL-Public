using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IGenreService
{
    public Task<IEnumerable<Genre>> GetGenres();
}