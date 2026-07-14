using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class PerformanceRepository : IPerformanceRepository
{
    private readonly AppDbContext _context;

    public PerformanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Performance>> GetAllPerformances()
    {
        return await _context.Performances.OrderBy(p => p.Title).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Performance>> GetAllPerformancesForAbout()
    {
        return await _context.Performances
            .Include(p => p.PlaceNavigation)
            .Include(p => p.GenreNavigation)
            .Include(p => p.PerformanceImplementers)
            .ThenInclude(pi => pi.Implementer)
            .Include(p => p.PerformanceEvents)
            .OrderBy(p => p.Title)
            .ToListAsync();
    }

    public async Task<Performance?> GetPerformanceById(int id)
    {
        return await _context.Performances.Include(p => p.GenreNavigation)
            .Include(p => p.PlaceNavigation)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Performance>> GetNewestPerformancePremieres()
    {
        return await _context.Performances
            .Where(p => p.PremiereDate != null)
            .OrderByDescending(p => p.PremiereDate)
            .Take(5)
            .ToListAsync();
    }
}