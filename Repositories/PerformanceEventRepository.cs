using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Repositories;

public class PerformanceEventRepository : GenericRepository<PerformanceEvent>, IPerformanceEventRepository
{
    public PerformanceEventRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<MinMaxDate?> GetMinMaxDateAsync()
    {
        return await _context.PerformanceEvents
            .GroupBy(_ => 1)
            .Select(pe =>
                new MinMaxDate
                {
                    MinDate = pe.Min(e => e.StartAt),
                    MaxDate = pe.Max(e => e.StartAt)
                }
            ).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PerformanceEventDto>> GetPerformanceEventsByMonthAsync(DateTime start, DateTime end)
    {
        return await _context.PerformanceEvents
            .AsNoTracking()
            .Where(pe => pe.StartAt >= start && pe.StartAt < end)
            .OrderBy(pe => pe.StartAt)
            .Select(pe => new PerformanceEventDto
            {
                Id = pe.Id,
                StartAt = pe.StartAt,
                PerformanceId = pe.PerformanceId,
                Title = pe.Performance.Title,
                Genre = pe.Performance.GenreNavigation.Name,
                Place = pe.Performance.PlaceNavigation.Name,
                MainImage =  pe.Performance.MainImage,
                BuyLink = pe.BuyLink ?? string.Empty,
                IsActive = pe.IsActive
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<PerformanceEvent>> GetNeatestSixPerformancesAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.PerformanceEvents
            .Include(pe => pe.Performance)
            .ThenInclude(p => p.PlaceNavigation)
            .Include(pe => pe.Performance)
            .ThenInclude(p => p.GenreNavigation)
            .Where(pe => pe.StartAt >= now)
            .OrderBy(pe => pe.StartAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<PerformanceEventDto>> GetNearestFiveEventsForPerformanceAsync(int performanceId)
    {
        var now = DateTime.UtcNow;
        return await _context.PerformanceEvents
            .AsNoTracking()
            .Where(pe => pe.PerformanceId == performanceId && pe.StartAt >= now)
            .OrderBy(pe => pe.StartAt)
            .Select(pe => new PerformanceEventDto
            {
                Id = pe.Id,
                StartAt = pe.StartAt,
                PerformanceId = pe.PerformanceId,
                Title = pe.Performance.Title,
                Genre = pe.Performance.GenreNavigation.Name,
                Place = pe.Performance.PlaceNavigation.Name,
                BuyLink = pe.BuyLink ?? string.Empty,
                IsActive = pe.IsActive
            })
            .Take(5)
            .ToListAsync();
    }

    // public async Task<PerformanceEvent?> GetEventByIdAsync(int id)
    // {
    //     var performanceEvent = await _context.PerformanceEvents
    //         .Include(pe => pe.Performance)
    //         .ThenInclude(p => p.GenreNavigation)
    //         .Include(pe => pe.Performance)
    //         .ThenInclude(p => p.PlaceNavigation)
    //         .FirstOrDefaultAsync(pe => pe.Id == id);
    //
    //     return performanceEvent;
    // }
}