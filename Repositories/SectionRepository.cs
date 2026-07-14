using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Repositories;


public class SectionRepository 
    : GenericRepository<Section>, ISectionRepository
{
    private readonly AppDbContext _context;

    public SectionRepository(AppDbContext context) 
        : base(context)
    {
        _context = context;
    }

    public async Task<Section?> GetBySlugAsync(string slug, SectionType sectionType)
    {
        if (slug == "main")
        {
            return await _context.Sections.FirstOrDefaultAsync(s => s.Type == sectionType && s.IsMain);
        }
        return await _context.Sections
            .FirstOrDefaultAsync(x => x.Slug == slug && x.Type == sectionType);
    }

    public async Task<List<Section>> GetVisibleOrderedAsync(SectionType sectionType)
    {
        return await _context.Sections
            .Where(x => x.IsVisible && x.Type == sectionType && !x.IsMain)
            .OrderBy(x => x.Order)
            .ToListAsync();
    }
}