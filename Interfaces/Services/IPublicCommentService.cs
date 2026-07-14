using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IPublicCommentService
{
    Task<IEnumerable<PublicCommentDto>> GetAllShowingAsync();
}