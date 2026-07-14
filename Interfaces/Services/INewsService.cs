using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface INewsService
{
    Task<IEnumerable<News>> GetAllNews();
    Task<IEnumerable<NewsGetDto>> GetFiveRecent();
    Task<News?> GetNewsById(int id);
}