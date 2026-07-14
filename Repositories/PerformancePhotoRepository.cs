using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class PerformancePhotoRepository : IPerformancePhotoRepository
{
    private readonly AppDbContext _context;

    public PerformancePhotoRepository(AppDbContext context)
    {
        _context = context;
    } 
    
    public async Task<IEnumerable<PerformancePhoto>> GetPerformancePhotosByPerformanceId(int performanceId)
    {
        return await _context.PerformancePhotos
            .Where(p => p.PerformanceId == performanceId)
            .ToListAsync();
    }
}