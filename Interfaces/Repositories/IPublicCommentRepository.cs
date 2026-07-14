using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IPublicCommentRepository
{
    Task<IEnumerable<PublicComment>> GetAllShowingAsync();
}