using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminPerformanceService : IAdminPerformanceService
{
    private readonly IAdminPerformanceRepository _repository;
    private readonly IGenreRepository _genreRepository;
    private readonly IPlaceRepository _placeRepository;
    private readonly IPerformanceConverter _performanceConverter;
    
    public AdminPerformanceService(IAdminPerformanceRepository repository, IGenreRepository genreRepository,
        IPlaceRepository placeRepository, IPerformanceConverter performanceConverter)
    {
        _repository = repository;
        _genreRepository = genreRepository;
        _placeRepository = placeRepository;
        _performanceConverter = performanceConverter;
    }
    
    public async Task<IEnumerable<Performance>> GetPerformances()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Performance?> GetPerformanceById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Performance?> CreatePerformanceAsync(PerformanceCreateUpdateDto performanceCreateUpdateDto)
    {
        var genre = await _genreRepository.GetGenreById(performanceCreateUpdateDto.Genre);
        if (genre == null)
        {
            return null;
        }
        var place = await _placeRepository.GetPlaceById(performanceCreateUpdateDto.Place);
        if (place == null)
        {
            return null;
        }

        var performance = _performanceConverter.ConvertFromViewModelToModel(performanceCreateUpdateDto);
        return await _repository.CreateAsync(performance);
    }

    public async Task<Performance?> UpdatePerformance(PerformanceCreateUpdateDto performanceCreateUpdateDto)
    {
        var genre = await _genreRepository.GetGenreById(performanceCreateUpdateDto.Genre);
        if (genre == null)
        {
            return null;
        }
        var place = await _placeRepository.GetPlaceById(performanceCreateUpdateDto.Place);
        if (place == null)
        {
            return null;
        }

        var performance = _performanceConverter.ConvertFromViewModelToModel(performanceCreateUpdateDto);
        return await _repository.UpdateAsync(performance);
    }

    public async Task<bool> DeletePerformance(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsPerformancesByGenre(Genre genre)
    {
        return await _repository.ExistsPerformanceByGenre(genre);
    }

    public async Task<bool> ExistsPerformancesByPlace(Place place)
    {
        return await _repository.ExistsPerformanceByPlace(place);
    }
}