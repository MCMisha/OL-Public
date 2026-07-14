using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class NewsService : INewsService
{
    private INewsRepository  _newsRepository;
    
    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }
    
    public async Task<IEnumerable<News>> GetAllNews()
    {
        return await _newsRepository.GetAllAsync();
    }

    public async Task<IEnumerable<NewsGetDto>> GetFiveRecent()
    {
        var news = await _newsRepository.GetFiveRecent();
        
        return news.Select(n => new NewsGetDto
        {
            Id =  n.Id,
            Title = n.Title,
            Subtitle = n.Subtitle,
            CreationDate = n.CreationDate,
        });;
    }
    
    public async Task<News?> GetNewsById(int id)
    {
        var news = await _newsRepository.GetByIdAsync(id);
        return news;
    }
}