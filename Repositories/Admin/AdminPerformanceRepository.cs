using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminPerformanceRepository : GenericRepository<Performance>, IAdminPerformanceRepository
{
    private readonly AppDbContext _context;

    public AdminPerformanceRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Performance>> GetPerformances() =>
        await _context.Performances
            .Include(p => p.GenreNavigation)
            .Include(p => p.PlaceNavigation)
            .ToListAsync();

    public async Task<Performance?> GetPerformanceById(int id) =>
        await _context.Performances
            .Include(p => p.GenreNavigation)
            .Include(p => p.PlaceNavigation)
            .FirstOrDefaultAsync(p => p.Id == id);

    public Task<bool> ExistsPerformanceByGenre(Genre genre) =>
        _context.Performances.AnyAsync(p => p.Genre == genre.Id);

    public Task<bool> ExistsPerformanceByPlace(Place place) =>
        _context.Performances.AnyAsync(p => p.Place == place.Id);
}