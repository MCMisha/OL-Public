using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class NewsRepository : GenericRepository<News>, INewsRepository
{
    public NewsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<News>> GetFiveRecent()
    {
        return await _context.News.AsNoTracking().OrderByDescending(n => n.CreationDate).Take(5).ToListAsync();
    }
}