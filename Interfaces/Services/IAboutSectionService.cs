using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IAboutSectionService
{
    Task<IEnumerable<SectionListGetDto>> GetAllAsync();
    Task<SectionDetailsGetDto?> GetBySlugAsync(string slug);
}
