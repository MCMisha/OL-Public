using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IPerformanceRepository
{
    Task<IEnumerable<Performance>> GetAllPerformances();
    Task<IEnumerable<Performance>> GetAllPerformancesForAbout();
    Task<Performance?> GetPerformanceById(int id);
    Task<IEnumerable<Performance>> GetNewestPerformancePremieres();
}