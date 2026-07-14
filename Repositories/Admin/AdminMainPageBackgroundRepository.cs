using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminMainPageBackgroundRepository(AppDbContext context)
    : GenericRepository<MainPageBackground>(context), IAdminMainPageBackgroundRepository
{
    public async Task<IEnumerable<MainPageBackground>> GetMainPageBackgroundsAsync()
    {
        return await _context.MainPageBackgrounds
            .Include(mb => mb.PerformanceNavigation)
            .ToListAsync();
    }
}