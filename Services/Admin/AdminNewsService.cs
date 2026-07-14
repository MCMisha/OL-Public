using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminNewsService : IAdminNewsService
{
    private readonly IAdminNewsRepository _adminNewsRepository;
    private readonly INewsConverter _newsConverter;

    public AdminNewsService(IAdminNewsRepository adminNewsRepository, INewsConverter newsConverter)
    {
        _adminNewsRepository = adminNewsRepository;
        _newsConverter = newsConverter;
    }
    
    public async Task<IEnumerable<News>> GetAllNewsAsync()
    {
        return await _adminNewsRepository.GetAllAsync();
    }

    public async Task<News?> GetNewsByIdAsync(int id)
    {
        var news = await _adminNewsRepository.GetByIdAsync(id);
        return news;
    }

    public async Task<News?> CreateNewsAsync(NewsCreateUpdateDto newsCreateUpdateDto)
    {
        var news = _newsConverter.ConvertFromViewModelToModel(newsCreateUpdateDto);
        return await _adminNewsRepository.CreateAsync(news);
    }

    public async Task<News?> UpdateNewsAsync(NewsCreateUpdateDto updatedNews)
    {
        var news = _newsConverter.ConvertFromViewModelToModel(updatedNews);
        return await _adminNewsRepository.UpdateAsync(news);
    }

    public async Task<bool> DeleteNewsAsync(int id)
    {
        var result = await _adminNewsRepository.DeleteAsync(id);
        return result;
    }
}