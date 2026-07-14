using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface ISectionRepository : IGenericRepository<Section>
{
    Task<Section?> GetBySlugAsync(string slug, SectionType sectionType);
    Task<List<Section>> GetVisibleOrderedAsync(SectionType sectionType);
}