using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class ArtistsRepository : GenericRepository<Artist>, IArtistsRepository
{
    public ArtistsRepository(AppDbContext context) : base(context)
    {
    }

    public new async Task<Artist?> GetByIdAsync(int id)
    {
        return await _context.Artists
            .AsNoTracking()
            .Where(a => a.Id == id)
            .Include(a => a.Photos)
            .Include(a => a.EventArtists)
            .ThenInclude(ea => ea.PerformanceEventNavigation)
            .ThenInclude(pe => pe.Performance)
            .ThenInclude(p => p.GenreNavigation)
            .Include(a => a.EventArtists)
            .ThenInclude(ea => ea.PerformanceEventNavigation)
            .ThenInclude(pe => pe.Performance)
            .ThenInclude(p => p.PlaceNavigation)
            .FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Artist>> GetCountAsync(int count)
    {
        return await _context.Artists.Take(count).ToListAsync();
    }
}