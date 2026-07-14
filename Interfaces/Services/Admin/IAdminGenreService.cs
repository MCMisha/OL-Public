using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminGenreService
{
    Task<IEnumerable<Genre>> GetGenres();
    Task<Genre?> GetGenreById(int id);
    Task<Genre?> CreateGenreAsync(Genre? genre);
    Task<Genre?> UpdateGenre(Genre genre);
    Task<bool> DeleteGenre(int id);
}