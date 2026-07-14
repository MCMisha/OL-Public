using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminPerformancePhotoRepository : GenericRepository<PerformancePhoto>, IAdminPerformancePhotoRepository
{
    public AdminPerformancePhotoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<PerformancePhoto>> GetPerformancePhotosByPerformanceId(int performanceId)
    {
        return await _context.PerformancePhotos
            .Where(p => p.PerformanceId == performanceId)
            .ToListAsync();
    }

    public async Task AddPerformancePhotosAsync(IEnumerable<PerformancePhoto> performancePhotos)
    {
        _context.PerformancePhotos.AddRange(performancePhotos);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePerformancePhotosAsync(int performanceId)
    {
        var photos = await _context.PerformancePhotos
            .Where(p => p.PerformanceId == performanceId)
            .ToListAsync();

        if (!photos.Any())
        {
            return;
        }
        
        _context.PerformancePhotos.RemoveRange(photos);
        await _context.SaveChangesAsync();
    }
}