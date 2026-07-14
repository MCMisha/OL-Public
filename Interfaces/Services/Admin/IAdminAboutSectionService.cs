using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminAboutSectionService
{
    Task<Section> CreateAsync(AboutSectionCreateDto aboutSection);
    Task<IEnumerable<SectionListGetDto>> GetAllAsync();
    Task<Section?> GetByIdAsync(int id);
    Task<SectionDetailsGetDto?> GetBySlugAsync(string slug);
    Task<Section?> UpdateAsync(Section section);
    Task UpdateOrderAsync(List<UpdateOrderRequest> model);
    Task<Section?> SetMainAsync(int id);
    Task<bool> DeleteAsync(int id);
}
