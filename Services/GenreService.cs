using WebApplicationOperaLublin.Interfaces;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    
    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    
    public async Task<IEnumerable<Genre>> GetGenres()
    {
        return await _genreRepository.GetGenres();
    }
}