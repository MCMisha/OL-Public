using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Converters;

public interface IArtistConverter
{
    Artist ConvertFromViewModelToModel(ArtistCreateUpdateDto createUpdateDto);
}
