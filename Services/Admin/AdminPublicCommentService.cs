using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminPublicCommentService : IAdminPublicCommentService
{
    private readonly IAdminPublicCommentRepository _repository;
    private readonly IPublicCommentConverter _converter;

    public AdminPublicCommentService(IAdminPublicCommentRepository adminPublicCommentRepository,
        IPublicCommentConverter commentConverter)
    {
        _repository = adminPublicCommentRepository;
        _converter = commentConverter;
    }

    public async Task<PublicComment?> CreateAsync(PublicCommentCreateUpdateDto? publicCommentVm)
    {
        if (publicCommentVm == null)
        {
            return null;
        }

        var publicComment = _converter.ConvertFromViewModelToModel(publicCommentVm);
        
        return await _repository.CreateAsync(publicComment);
    }

    public async Task<IEnumerable<PublicComment>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<PublicComment?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<PublicComment?> UpdateAsync(PublicCommentCreateUpdateDto? publicCommentVm)
    {
        if (publicCommentVm == null)
        {
            return null;
        }
        
        var publicComment = _converter.ConvertFromViewModelToModel(publicCommentVm);
        
        return await _repository.UpdateAsync(publicComment);
    }

    public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
}