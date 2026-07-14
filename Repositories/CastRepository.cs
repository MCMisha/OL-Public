using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class CastRepository : ICastRepository
{
    private readonly AppDbContext _context;

    public CastRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<EventArtist>> GetCastByPerformance(int performanceId)
    {
        return await _context.EventArtists
            .Include(pa => pa.Artist)
            .Include(pa => pa.PerformanceEventNavigation)
            .Where(pa => pa.PerformanceEventNavigation.PerformanceId == performanceId)
            .ToListAsync();
    }
}