using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IMainPageBackgroundService
{
    Task<IEnumerable<MainPageBackgroundDto>> GetAllActiveMainPageBackgrounds();
}