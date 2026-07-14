using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Converters;

public class ArtistConverter : IArtistConverter
{
    public Artist ConvertFromViewModelToModel(ArtistCreateUpdateDto createUpdateDto)
    {
        return new Artist
        {
            Id = createUpdateDto.Id,
            FirstName = createUpdateDto.FirstName,
            LastName = createUpdateDto.LastName,
            Description = createUpdateDto.Description,
            Photo = string.IsNullOrEmpty(createUpdateDto.Photo)
                ? null!
                : Convert.FromBase64String(createUpdateDto.Photo),
            Category = (ArtistCategory)createUpdateDto.Category
        };
    }
}
