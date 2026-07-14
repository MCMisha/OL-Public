using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminTicketPriceRepository : IAdminTicketPriceRepository
{
    private readonly AppDbContext _context;

    public AdminTicketPriceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketPriceGroup>> GetGroupsWithPricesAsync(int performanceId)
    {
        return await _context.TicketPriceGroups
            .Where(g => g.PerformanceId == performanceId)
            .Include(g => g.Prices)
            .OrderBy(g => g.SortOrder)
            .ToListAsync();
    }

    public async Task ReplacePriceGroupsAsync(int performanceId, List<TicketPriceGroup> newGroups)
    {
        await using var tx = await _context.Database.BeginTransactionAsync();

        var oldGroups = await _context.TicketPriceGroups
            .Where(g => g.PerformanceId == performanceId)
            .Include(g => g.Prices)
            .ToListAsync();
        
        var oldPrices = oldGroups.SelectMany(g => g.Prices).ToList();
        if (oldPrices.Count > 0)
            _context.TicketPrices.RemoveRange(oldPrices);

        if (oldGroups.Count > 0)
            _context.TicketPriceGroups.RemoveRange(oldGroups);

        await _context.TicketPriceGroups.AddRangeAsync(newGroups);

        await _context.SaveChangesAsync();
        await tx.CommitAsync();
    }
}