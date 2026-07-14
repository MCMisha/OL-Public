using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IArtistsService
{
    Task<IEnumerable<Artist>> GetArtists();
    Task<IEnumerable<Artist>> GetArtistsMain();
    Task<ArtistWithPhotosAndPerformancesDto?> GetArtistById(int id);
}