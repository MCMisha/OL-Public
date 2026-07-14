using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminAboutSectionRepository : IGenericRepository<Section>
{
    Task<Section?> GetBySlugAsync(string slug);
    Task<Section?> SetMainAsync(Section section);
}
