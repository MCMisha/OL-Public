using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminPerformanceInfoService : IAdminPerformanceInfoService
{
    private readonly IAdminPerformanceInfoRepository _repository;
    private readonly IAdminPerformanceRepository _performanceRepository;
    private readonly IImplementerConverter _implementerConverter;

    public AdminPerformanceInfoService(IAdminPerformanceInfoRepository repository,
        IAdminPerformanceRepository performanceRepository, IImplementerConverter implementerConverter)
    {
        _repository = repository;
        _performanceRepository = performanceRepository;
        _implementerConverter = implementerConverter;
    }

    public async Task<IEnumerable<Implementer>> AddImplementers(IEnumerable<ImplementerCreateUpdateDto> implementersViewModel,
        int performanceId)
    {
        await ValidatePerformanceExists(performanceId);
        var implementers = implementersViewModel.Select(implementerViewModel =>
            _implementerConverter.ConvertFromViewModelToModel(implementerViewModel));
        return await _repository.AddImplementers(implementers, performanceId);
    }

    public async Task<IEnumerable<ImplementerCreateUpdateDto>> GetImplementersByPerformance(int performanceId)
    {
        await ValidatePerformanceExists(performanceId);
        var implementers = await _repository.GetImplementersByPerformance(performanceId);

        return implementers.Select(implementer => _implementerConverter.ConvertFromModelToViewModel(implementer));
    }

    public async Task<ImplementerCreateUpdateDto?> GetImplementerById(int performanceId, int implementerId)
    {
        await ValidatePerformanceExists(performanceId);
        var implementer = await _repository.GetImplementerById(performanceId, implementerId);
        if (implementer != null)
        {
            return _implementerConverter.ConvertFromModelToViewModel(implementer);
        }

        return null;
    }

    public async Task<Implementer> UpdateImplementer(int performanceId, int implementerId,
        ImplementerCreateUpdateDto implementerCreateUpdateDto)
    {
        await ValidatePerformanceExists(performanceId);
        var implementer = await _repository.GetImplementerById(performanceId, implementerId);
        if (implementer == null)
        {
            throw new ImplementerNotFoundException(implementerId);
        }

        return await _repository.UpdateImplementer(performanceId, implementerId,
            _implementerConverter.ConvertFromViewModelToModel(implementerCreateUpdateDto));
    }

    public async Task<bool> DeleteImplementer(int performanceId, int implementerId)
    {
        await ValidatePerformanceExists(performanceId);
        return await _repository.DeleteImplementer(performanceId, implementerId);
    }

    private async Task ValidatePerformanceExists(int performanceId)
    {
        var performance = await _performanceRepository.GetByIdAsync(performanceId);
        if (performance == null)
        {
            throw new PerformanceNotFoundException(performanceId);
        }
    }
}