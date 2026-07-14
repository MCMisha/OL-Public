using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services;

public class PerformanceInfoService : IPerformanceInfoService
{
    private readonly IPerformanceInfoRepository _repository;
    private readonly IPerformanceRepository _performanceRepository;
    private readonly IImplementerConverter _implementerConverter;

    public PerformanceInfoService(IPerformanceInfoRepository repository, IPerformanceRepository performanceRepository,
        IImplementerConverter implementerConverter)
    {
        _repository = repository;
        _performanceRepository = performanceRepository;
        _implementerConverter = implementerConverter;
    }

    public async Task<IEnumerable<ImplementerCreateUpdateDto>> GetImplementersByPerformance(int performanceId)
    {
        await ValidatePerformanceExists(performanceId);
        var implementers = await _repository.GetImplementersByPerformance(performanceId);
        
        return implementers.OrderBy(implementer => implementer.Id).Select(implementer => _implementerConverter.ConvertFromModelToViewModel(implementer));
    }

    private async Task ValidatePerformanceExists(int performanceId)
    {
        var performance = await _performanceRepository.GetPerformanceById(performanceId);
        if (performance == null)
        {
            throw new PerformanceNotFoundException(performanceId);
        }
    }
}