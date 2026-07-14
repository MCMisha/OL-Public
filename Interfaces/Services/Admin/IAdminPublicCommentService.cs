using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services.Admin;

public interface IAdminPublicCommentService
{   
    Task<PublicComment?> CreateAsync(PublicCommentCreateUpdateDto? publicCommentVm);
    Task<IEnumerable<PublicComment>> GetAllAsync();
    Task<PublicComment?> GetByIdAsync(int id);
    Task<PublicComment?> UpdateAsync(PublicCommentCreateUpdateDto? publicComment);
    Task<bool> DeleteAsync(int id);
}