using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IPlaceRepository
{
    Task<Place?> GetPlaceById(int id);
    Task<IEnumerable<Place>> GetPlaces();
}