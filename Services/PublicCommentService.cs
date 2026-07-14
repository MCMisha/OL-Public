using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class PublicCommentService : IPublicCommentService
{
    private readonly IPublicCommentRepository  _repository;

    public PublicCommentService(IPublicCommentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<PublicCommentDto>> GetAllShowingAsync()
    {
        var comments = await _repository.GetAllShowingAsync();

        return comments.Select(c => new PublicCommentDto
        {
            Photo = c.Photo,
            FirstName = c.FirstName,
            Stars = c.Stars,
            PerformanceTitle = c.Performance.Title,
            Comment = c.Comment,
            DatePublished = c.DatePublished
        });
    }
}