using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminArtistsService
{
    Task<IEnumerable<Artist>> GetArtists();
    Task<Artist?> GetArtistById(int id);
    Task<Artist> CreateArtistAsync(ArtistCreateUpdateDto artistCreateUpdateDto);
    Task<Artist?> UpdateArtist(ArtistCreateUpdateDto artistCreateUpdateDto);
    Task<bool> DeleteArtist(int id);
}