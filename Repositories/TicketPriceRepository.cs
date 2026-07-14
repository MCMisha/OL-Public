using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class TicketPriceRepository: ITicketPriceRepository
{
    private readonly AppDbContext _context;

    public TicketPriceRepository(AppDbContext context)
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
}