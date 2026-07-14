using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminPerformanceService
{
    Task<IEnumerable<Performance>> GetPerformances();
    Task<Performance?> GetPerformanceById(int id);
    Task<Performance?> CreatePerformanceAsync(PerformanceCreateUpdateDto performanceCreateUpdateDto);
    Task<Performance?> UpdatePerformance(PerformanceCreateUpdateDto performance);
    Task<bool> DeletePerformance(int id);
    Task<bool> ExistsPerformancesByGenre(Genre genre);
    Task<bool> ExistsPerformancesByPlace(Place place);
}