using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories.Admin;

public interface IAdminMainPageBackgroundRepository : IGenericRepository<MainPageBackground>
{
    Task<IEnumerable<MainPageBackground>> GetMainPageBackgroundsAsync(); 
}