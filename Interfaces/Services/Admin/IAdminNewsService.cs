using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminNewsService
{
    Task<IEnumerable<News>> GetAllNewsAsync();
    Task<News?> GetNewsByIdAsync(int id);
    Task<News?> CreateNewsAsync(NewsCreateUpdateDto news);
    Task<News?> UpdateNewsAsync(NewsCreateUpdateDto updatedNews);
    Task<bool> DeleteNewsAsync(int id);
}