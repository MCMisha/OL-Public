using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminCastRepository : IAdminCastRepository
{
    private readonly AppDbContext _context;

    public AdminCastRepository(AppDbContext context)
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

    public async Task AddCast(IEnumerable<EventArtist> cast)
    {
        await _context.EventArtists.AddRangeAsync(cast);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteCast(int eventId, int artistId)
    {
        var entity = await _context.EventArtists
            .FirstOrDefaultAsync(pa =>
                pa.EventId == eventId &&
                pa.ArtistId == artistId);

        if (entity == null)
            return false;

        _context.EventArtists.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}