using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Converters;

public class PublicCommentConverter : IPublicCommentConverter
{
    public PublicComment ConvertFromViewModelToModel(PublicCommentCreateUpdateDto createUpdateDto)
    {
        return new PublicComment
        {
            Id = createUpdateDto.Id,
            Photo = string.IsNullOrEmpty(createUpdateDto.Photo) ? null! : Convert.FromBase64String(createUpdateDto.Photo),
            FirstName = createUpdateDto.FirstName,
            Stars = createUpdateDto.Stars,
            PerformanceId = createUpdateDto.PerformanceId,
            Comment = createUpdateDto.Comment,
            DatePublished = createUpdateDto.DatePublished,
            IsShowing = createUpdateDto.IsShowing
        };
    }
}