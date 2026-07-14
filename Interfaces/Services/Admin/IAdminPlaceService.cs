using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminPlaceService
{
    Task<IEnumerable<Place>> GetPlaces();
    Task<Place?> GetPlaceById(int id);
    Task<Place?> CreatePlaceAsync(Place? place);
    Task<Place?> UpdatePlace(Place place);
    Task<bool> DeletePlace(int id);
}