using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IContactSectionService
{
    Task<IEnumerable<SectionListGetDto>> GetAllAsync();
    Task<SectionDetailsGetDto?> GetBySlugAsync(string slug);
}