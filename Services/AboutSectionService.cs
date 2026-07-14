using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Services;

public class AboutSectionService : IAboutSectionService
{
    private readonly ISectionRepository _repo;

    public AboutSectionService(ISectionRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<SectionListGetDto>> GetAllAsync()
    {
        var items = await _repo.GetVisibleOrderedAsync(SectionType.About);

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
        var item = await _repo.GetBySlugAsync(slug, SectionType.About);
        if (item == null) return null;

        return new SectionDetailsGetDto
        {
            Id = item.Id,
            Title = item.Title,
            ContentHtml = item.ContentHtml
        };
    }
}
