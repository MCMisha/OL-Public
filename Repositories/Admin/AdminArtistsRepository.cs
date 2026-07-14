using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminArtistsRepository : GenericRepository<Artist>, IAdminArtistsRepository
{
    public AdminArtistsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Artist>> GetCountAsync(int count)
    {
        return await _context.Artists.Take(count).ToListAsync();
    }
}