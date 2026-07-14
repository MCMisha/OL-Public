using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Converters;

public class PerformanceConverter : IPerformanceConverter
{
    public Performance ConvertFromViewModelToModel(PerformanceCreateUpdateDto createUpdateDto)
    {
        return new Performance
        {
            Id = createUpdateDto.Id,
            Title = createUpdateDto.Title,
            Genre = createUpdateDto.Genre,
            Place = createUpdateDto.Place,
            BreaksCount = createUpdateDto.BreaksCount,
            MainImage = string.IsNullOrEmpty(createUpdateDto.MainImage) ? null! : Convert.FromBase64String(createUpdateDto.MainImage)!,
            Poster = string.IsNullOrEmpty(createUpdateDto.Poster) ? null : Convert.FromBase64String(createUpdateDto.Poster),
            Description = createUpdateDto.Description,
            Duration = TimeOnly.Parse(createUpdateDto.Duration),
            PremiereDate = createUpdateDto.PremiereDate
        };
    }
}