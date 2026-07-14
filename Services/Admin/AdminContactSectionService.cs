using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminContactSectionService : IAdminContactSectionService
{
    private IAdminContactSectionRepository _repository;

    public AdminContactSectionService(IAdminContactSectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Section> CreateAsync(AboutSectionCreateDto aboutSectionViewModel)
    {
        var aboutSection = new Section
        {
            Title = aboutSectionViewModel.Title,
            Slug = aboutSectionViewModel.Slug,
            ContentHtml = aboutSectionViewModel.ContentHtml,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsVisible = true,
            Order = aboutSectionViewModel.Order,
            Type = SectionType.Contact
        };
        return await _repository.CreateAsync(aboutSection);
    }

    public async Task<IEnumerable<SectionListGetDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();

        return items.Select(x => new SectionListGetDto
        {
            Id = x.Id,
            Title = x.Title,
            Slug = x.Slug,
            Order = x.Order,
            IsVisible = x.IsVisible,
            IsMain = x.IsMain
        }).OrderBy(x => x.Order).ToList();
    }

    public async Task<SectionDetailsGetDto?> GetBySlugAsync(string slug)
    {
        
        var item = await _repository.GetBySlugAsync(slug);
        if (item == null) return null;

        return new SectionDetailsGetDto
        {
            Id = item.Id,
            Title = item.Title,
            ContentHtml = item.ContentHtml,
            Slug = item.Slug,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };
    }

    public async Task<Section?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Section?> UpdateAsync(Section section)
    {
        var entity = await _repository.GetByIdAsync(section.Id);
        if (entity == null)
            return null;

        var slugOwner = await _repository.GetBySlugAsync(section.Slug);
        if (slugOwner != null && slugOwner.Id != section.Id)
        {
            throw new Exception("Slug already exists");   
        }

        entity.Title = section.Title;
        entity.Slug = section.Slug;
        entity.ContentHtml = section.ContentHtml;
        entity.Order = section.Order;
        entity.IsVisible = section.IsVisible;
        entity.UpdatedAt = DateTime.UtcNow;

        return await _repository.UpdateAsync(entity);
    }

    public async Task UpdateOrderAsync(List<UpdateOrderRequest> model)
    {
        var ids = model.Select(x => x.Id);

        var entities = await _repository.GetByIdsAsync(ids);

        foreach (var entity in entities)
        {
            entity.Order = model.First(x => x.Id == entity.Id).Order;
        }

        await _repository.UpdateRangeAsync(entities);
    }

    public async Task<Section?> SetMainAsync(int id)
    {
        var section = await _repository.GetByIdAsync(id);
        if (section == null)
        {
            throw new AboutSectionNotFoundException(id);
        }

        section.IsMain = true;
        return await _repository.SetMainAsync(section);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}