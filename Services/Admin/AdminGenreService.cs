using WebApplicationOperaLublin.Interfaces;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminGenreService : IAdminGenreService
{
    private readonly IAdminGenreRepository _repository;

    public AdminGenreService(IAdminGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    
    public async Task<IEnumerable<Genre>> GetGenres()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Genre?> CreateGenreAsync(Genre? genre)
    {
        if (genre == null)
        {
            return genre;
        }
        return await _repository.CreateAsync(genre);
    }

    public async Task<Genre?> UpdateGenre(Genre? genre)
    {
        if (genre == null)
        {
            return genre;
        }
        return await _repository.UpdateAsync(genre)!;
    }

    public async Task<bool> DeleteGenre(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
