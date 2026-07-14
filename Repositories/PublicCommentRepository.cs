using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class PublicCommentRepository : IPublicCommentRepository
{
    private readonly AppDbContext _context;

    public PublicCommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PublicComment>> GetAllShowingAsync()
    {
        return await _context.PublicComments
            .AsNoTracking()
            .Include(pc => pc.Performance)
            .Where(pc => pc.IsShowing)
            .ToListAsync();
    }
}