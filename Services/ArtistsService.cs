using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class ArtistsService : IArtistsService
{
    private IArtistsRepository _artistsRepository;

    public ArtistsService(IArtistsRepository artistsRepository)
    {
        _artistsRepository = artistsRepository;
    }

    public async Task<IEnumerable<Artist>> GetArtists()
    {
        return await _artistsRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Artist>> GetArtistsMain()
    {
        return await _artistsRepository.GetCountAsync(10);
    }

    public async Task<ArtistWithPhotosAndPerformancesDto?> GetArtistById(int id)
    {
        var artist = await _artistsRepository.GetByIdAsync(id);
        if (artist == null)
        {
            throw new ArtistNotFoundException(id);
        }

        return new ArtistWithPhotosAndPerformancesDto
        {
            Id = artist.Id,
            FullName = $"{artist.FirstName} {artist.LastName}",
            Photo = artist.Photo,
            Category = artist.Category,
            Description = artist.Description ?? string.Empty,
            ArtistPhotos = artist.Photos.Select(aph => aph.Photo),
            ArtistEvents = artist.EventArtists.Select(ea => ea.PerformanceEventNavigation).Select(pe =>
                new PerformanceEventWithImageDto
                {
                    EventId = pe.Id,
                    StartAt = pe.StartAt,
                    PerformanceId = pe.PerformanceId,
                    Title = pe.Performance.Title,
                    Genre = pe.Performance.GenreNavigation.Name,
                    Place = pe.Performance.PlaceNavigation.Name,
                    MainImage = pe.Performance.MainImage,
                    BuyLink = pe.BuyLink ?? string.Empty,
                    IsActive = pe.IsActive
                })
        };
    }
}