using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminMainPageBackgroundService : IAdminMainPageBackgroundService
{
    private IAdminMainPageBackgroundRepository _repository;

    public AdminMainPageBackgroundService(IAdminMainPageBackgroundRepository repository)
    {
        _repository = repository;
    }

    public async Task<MainPageBackground?> CreateMainPageBackgroundAsync(MainPageBackground mainPageBackground)
    {
        return await _repository.CreateAsync(mainPageBackground);
    }

    public async Task<IEnumerable<AdminMainPageBackgroundDto>> GetMainPageBackgroundsAsync()
    {
        var backgrounds = await _repository.GetMainPageBackgroundsAsync();
        return backgrounds.Select(mbg =>
            new AdminMainPageBackgroundDto
            {
                Id = mbg.Id,
                Title = mbg.PerformanceNavigation.Title,
                CreatedAt = mbg.CreatedAt,
                DisplayOrder = mbg.DisplayOrder,
                IsActive = mbg.IsActive,
                MainImage = mbg.MainImage
            });
    }

    public async Task<MainPageBackground?> GetMainPageBackgroundByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<MainPageBackground?> UpdateMainPageBackgroundAsync(MainPageBackground mainPageBackground)
    {
        return await _repository.UpdateAsync(mainPageBackground);
    }

    public async Task<bool> DeleteMainPageBackgroundAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}