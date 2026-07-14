using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminAboutSectionRepository : GenericRepository<Section>, IAdminAboutSectionRepository
{
    public AdminAboutSectionRepository(AppDbContext context) : base(context) { }

    public new async Task<IEnumerable<Section>> GetAllAsync()
    {
        return await _context.Sections.Where(s => s.Type == SectionType.About).ToListAsync();
    }
    
    public async Task<Section?> GetBySlugAsync(string slug)
    {
        if (slug == "main")
        {
            return await _context.Sections.FirstOrDefaultAsync(s => s.Type == SectionType.Contact && s.IsMain);
        }
        return await _context.Sections.FirstOrDefaultAsync(e => e.Slug.ToLower().Equals(slug.ToLower()));
    }

    public async Task<Section> SetMainAsync(Section section)
    {
        var existingSection = await _context.Sections
            .FirstOrDefaultAsync(s => s.Id == section.Id);

        if (existingSection == null)
        {
            return null;
        }

        if (section.IsMain)
        {
            var oldMainSections = await _context.Sections
                .Where(s => s.Type == section.Type 
                            && s.Id != section.Id 
                            && s.IsMain)
                .ToListAsync();

            foreach (var oldMain in oldMainSections)
            {
                oldMain.IsMain = false;
            }
        }

        _context.Entry(existingSection).CurrentValues.SetValues(section);

        await _context.SaveChangesAsync();

        return existingSection;
    }
}
