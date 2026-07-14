using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminMainPageBackgroundService
{
    Task<MainPageBackground?> CreateMainPageBackgroundAsync(MainPageBackground mainPageBackground);
    Task<IEnumerable<AdminMainPageBackgroundDto>> GetMainPageBackgroundsAsync();
    Task<MainPageBackground?>  GetMainPageBackgroundByIdAsync(int id);
    Task<MainPageBackground?>  UpdateMainPageBackgroundAsync(MainPageBackground mainPageBackground);
    Task<bool>  DeleteMainPageBackgroundAsync(int id);
}