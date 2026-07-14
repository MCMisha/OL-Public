using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class MainPagePageBackgroundRepository : IMainPageBackgroundRepository
{
    private readonly AppDbContext _context;

    public MainPagePageBackgroundRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<MainPageBackground>> GetAllActive()
    {
        return await _context.MainPageBackgrounds
            .Where(x => x.IsActive)
            .Include(x => x.PerformanceNavigation)
            .ThenInclude(p => p.GenreNavigation)
            .Include(x => x.PerformanceNavigation)
            .ThenInclude(p => p.PerformanceEvents)
            .ToListAsync();
    }
}