using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Repositories.Admin;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminPlaceService : IAdminPlaceService
{
    private readonly IAdminPlaceRepository _repository;
    public AdminPlaceService(IAdminPlaceRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Place>> GetPlaces() => await _repository.GetAllAsync();
    public async Task<Place?> GetPlaceById(int id) => await _repository.GetByIdAsync(id);

    public async Task<Place?> CreatePlaceAsync(Place? place)
    {
        if (place == null)
        {
            return place;
        }
        return await _repository.CreateAsync(place)!;   
    }
    public async Task<Place?> UpdatePlace(Place? place) => await _repository.UpdateAsync(place)!;
    public async Task<bool> DeletePlace(int id) => await _repository.DeleteAsync(id);
}