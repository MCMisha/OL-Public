using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;
    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> GetGenres()
    {
        return await _context.Genres.ToListAsync();
    }
    public async Task<Genre?> GetGenreById(int genreId)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
    }

    public async Task<IEnumerable<Genre>> GetGenresByIds(List<int> ids)
    {
        return await _context.Genres.Where(g => ids.Contains(g.Id)).ToListAsync();
    }
}