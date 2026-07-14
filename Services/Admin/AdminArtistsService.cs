using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminArtistsService : IAdminArtistsService
{
    private IAdminArtistsRepository _adminArtistsRepository;
    private IArtistConverter _artistConverter;

    public AdminArtistsService(IAdminArtistsRepository adminArtistsRepository, IArtistConverter artistConverter)
    {
        _adminArtistsRepository = adminArtistsRepository;
        _artistConverter = artistConverter;
    }
    
    public async Task<IEnumerable<Artist>> GetArtists()
    {
        return await _adminArtistsRepository.GetAllAsync();
    }

    public async Task<Artist?> GetArtistById(int id)
    {
        return await _adminArtistsRepository.GetByIdAsync(id);
    }

    public async Task<Artist> CreateArtistAsync(ArtistCreateUpdateDto artistCreateUpdateDto)
    {
        if (!Enum.IsDefined(typeof(ArtistCategory), artistCreateUpdateDto.Category))
        {
            throw new InvalidArtistCategoryException((int)artistCreateUpdateDto.Category);
        }
            
        var artist = _artistConverter.ConvertFromViewModelToModel(artistCreateUpdateDto);
        return await _adminArtistsRepository.CreateAsync(artist);
    }

    public async Task<Artist?> UpdateArtist(ArtistCreateUpdateDto artistCreateUpdateDto)
    {
        if (!Enum.IsDefined(typeof(ArtistCategory), artistCreateUpdateDto.Category))
        {
            throw new InvalidArtistCategoryException((int)artistCreateUpdateDto.Category);
        }
        var artist = _artistConverter.ConvertFromViewModelToModel(artistCreateUpdateDto);
        return await _adminArtistsRepository.UpdateAsync(artist);
    }

    public async Task<bool> DeleteArtist(int id)
    {
        var result = await _adminArtistsRepository.DeleteAsync(id);
        return result;
    }
}