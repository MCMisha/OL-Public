using System.Globalization;
using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminPerformanceEventRepository : GenericRepository<PerformanceEvent>, IAdminPerformanceEventRepository
{
    public AdminPerformanceEventRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AdminPerformanceEventListDto>> GetAllPerformanceEventsAsync()
    {
        return await _context.PerformanceEvents
            .AsNoTracking()
            .OrderByDescending(pe => pe.StartAt)
            .Select(pe => new AdminPerformanceEventListDto
            {
                Id = pe.Id,
                StartAt = pe.StartAt,
                BuyLink = pe.BuyLink,
                PerformanceId = pe.PerformanceId,
                PerformanceTitle = pe.Performance.Title,
                GenreName = pe.Performance.GenreNavigation.Name,
                PlaceName = pe.Performance.PlaceNavigation.Name
            })
            .ToListAsync();
    }
    
    public async Task<PerformanceEvent?> GetEventByIdAsync(int id)
    {
        var performanceEvent = await _context.PerformanceEvents
            .Include(pe => pe.Performance)
            .ThenInclude(p => p.GenreNavigation)
            .Include(pe => pe.Performance)
            .ThenInclude(p => p.PlaceNavigation)
            .FirstOrDefaultAsync(pe => pe.Id == id);

        return performanceEvent;
    }

    public async Task<IEnumerable<AdminPerformanceEventListDto>> GetByPerformanceId(int performanceId)
    {
        return await _context.PerformanceEvents
            .AsNoTracking()
            .Where(pe => pe.PerformanceId == performanceId)
            .OrderByDescending(pe => pe.StartAt)
            .Select(pe => new AdminPerformanceEventListDto
            {
                Id = pe.Id,
                StartAt = pe.StartAt,
                BuyLink = pe.BuyLink ?? string.Empty,
                IsActive = pe.IsActive,
                PerformanceId = pe.PerformanceId,
                PerformanceTitle = pe.Performance.Title,
                GenreName = pe.Performance.GenreNavigation.Name,
                PlaceName = pe.Performance.PlaceNavigation.Name
            })
            .ToListAsync();
    }

    public async Task<AdminPerformanceEventListDto?> CreateEventAsync(PerformanceEvent entity)
    {
        _context.PerformanceEvents.Add(entity);
        await _context.SaveChangesAsync();

        return await _context.PerformanceEvents
            .AsNoTracking()
            .Where(pe => pe.Id == entity.Id)
            .Select(pe => new AdminPerformanceEventListDto
            {
                Id = pe.Id,
                PerformanceId = pe.PerformanceId,
                StartAt = pe.StartAt,
                BuyLink = pe.BuyLink ?? "",
                IsActive = pe.IsActive,
                PerformanceTitle = pe.Performance.Title,
                GenreName = pe.Performance.GenreNavigation.Name,
                PlaceName = pe.Performance.PlaceNavigation.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PerformanceEventUpdateDto?> UpdateEventAsync(PerformanceEvent entity)
    {
        _context.PerformanceEvents.Update(entity);
        await _context.SaveChangesAsync();

        return await _context.PerformanceEvents
            .AsNoTracking()
            .Where(pe => pe.Id == entity.Id)
            .Select(pe => new PerformanceEventUpdateDto
            {
                Id = pe.Id,
                PerformanceId = pe.PerformanceId,
                StartAt = pe.StartAt,
                BuyLink = pe.BuyLink ?? string.Empty,
                IsActive = pe.IsActive,
                CreatedAt = pe.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task UpdatePerformanceEventsForPerformance(int performanceId, List<PerformanceEvent> newEvents)
    {
        await using var tx = await _context.Database.BeginTransactionAsync();

        var oldEvents = await _context.PerformanceEvents
            .Where(e => e.PerformanceId == performanceId)
            .ToListAsync();

        if (oldEvents.Count > 0)
            _context.PerformanceEvents.RemoveRange(oldEvents);
        
        foreach (var e in newEvents)
        {
            e.Id = 0;
            e.PerformanceId = performanceId;

            if (e.StartAt.Kind != DateTimeKind.Utc)
                e.StartAt = DateTime.SpecifyKind(e.StartAt, DateTimeKind.Utc);

            e.CreatedAt = DateTime.UtcNow;
        }

        await _context.PerformanceEvents.AddRangeAsync(newEvents);

        await _context.SaveChangesAsync();
        await tx.CommitAsync();
    }
}