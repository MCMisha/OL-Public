using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Services;

public class ContactSectionService : IContactSectionService
{
    private readonly ISectionRepository _sectionRepository;

    public ContactSectionService(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }
    
    public async Task<IEnumerable<SectionListGetDto>> GetAllAsync()
    {
        var items = await _sectionRepository.GetVisibleOrderedAsync(SectionType.Contact);

        return items.Select(x => new SectionListGetDto
        {
            Id = x.Id,
            Title = x.Title,
            Slug = x.Slug,
            Order = x.Order,
            IsVisible = x.IsVisible
        }).OrderBy(x => x.Order).ToList();
    }

    public async Task<SectionDetailsGetDto?> GetBySlugAsync(string slug)
    {
        var item = await _sectionRepository.GetBySlugAsync(slug, SectionType.Contact);
        if (item == null) return null;

        return new SectionDetailsGetDto
        {
            Id = item.Id,
            Title = item.Title,
            ContentHtml = item.ContentHtml
        };
    }
}