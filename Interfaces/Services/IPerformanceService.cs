using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IPerformanceService
{
    Task<IEnumerable<Performance>> GetAllPerformances();
    Task<IEnumerable<PerformanceListAboutDto>> GetAllPerformanceListAbout();
    Task<Performance?> GetPerformanceById(int id);
    Task<IEnumerable<Performance>> GetNewestPerformancePremieres();
}