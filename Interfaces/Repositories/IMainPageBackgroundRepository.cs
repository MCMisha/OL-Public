using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IMainPageBackgroundRepository
{
    Task<IEnumerable<MainPageBackground>> GetAllActive();
}