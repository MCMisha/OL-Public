using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Models.Enums;

namespace WebApplicationOperaLublin.Converters;

public class NewsConverter : INewsConverter
{
    public News ConvertFromViewModelToModel(NewsCreateUpdateDto createUpdateDto)
    {
        return new News
        {
            Id = createUpdateDto.Id,
            Title = createUpdateDto.Title,
            Subtitle = createUpdateDto.Subtitle,
            MainImage = string.IsNullOrEmpty(createUpdateDto.MainImage)
                ? null!
                : Convert.FromBase64String(createUpdateDto.MainImage)!,
            Content = createUpdateDto.Content,
            CreationDate = createUpdateDto.CreationDate,
            Category = (NewsCategory)createUpdateDto.Category
        };
    }
}