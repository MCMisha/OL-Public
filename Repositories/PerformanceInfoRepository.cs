using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class PerformanceInfoRepository : IPerformanceInfoRepository
{
    private readonly AppDbContext _context;

    public PerformanceInfoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Implementer>> GetImplementersByPerformance(int performanceId)
    {
        return await _context.PerformanceImplementers
            .Where(pi => pi.PerformanceId == performanceId)
            .Include(pi => pi.Implementer)
            .Select(pi => pi.Implementer)
            .ToListAsync();
    }
}