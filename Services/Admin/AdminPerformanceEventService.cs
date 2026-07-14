using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Services.Admin;

public class AdminPerformanceEventService: IAdminPerformanceEventService
{
    private readonly IAdminPerformanceEventRepository _repository;
    private readonly IPerformanceRepository _performanceRepository;
    private readonly IPerformanceEventConverter _performanceEventConverter;
    
    public AdminPerformanceEventService(IAdminPerformanceEventRepository adminPerformanceEventRepository,
        IPerformanceRepository adminPerformanceRepository, IPerformanceEventConverter performanceEventConverter)
    {
        _repository = adminPerformanceEventRepository;
        _performanceRepository = adminPerformanceRepository;
        _performanceEventConverter = performanceEventConverter;
    }
    
    public async Task<IEnumerable<AdminPerformanceEventListDto>> GetAllEvents()
    {
        return await _repository.GetAllPerformanceEventsAsync();
    }

    public async Task<IEnumerable<AdminPerformanceEventListDto>> GetByPerformanceId(int performanceId)
    {
        var performance = await _performanceRepository.GetPerformanceById(performanceId);
        if (performance == null)
        {
            throw new PerformanceNotFoundException(performanceId);
        }

        var events = await _repository.GetByPerformanceId(performanceId);
        return events.OrderBy(e => e.StartAt);
    }

    public async Task<PerformanceEventDto?> GetEventById(int id)
    {
        var entity = await _repository.GetEventByIdAsync(id);
        if (entity == null)
        {
            return null;
        }
        return _performanceEventConverter.ConvertFromPerformanceEventToPerformanceEventDto(entity);
    }

    public async Task<AdminPerformanceEventListDto?> CreateEvent(PerformanceEventCreateUpdateDto newEvent)
    {
        var performance = await _performanceRepository.GetPerformanceById(newEvent.PerformanceId);
        if (performance == null)
        {
            throw new PerformanceNotFoundException(newEvent.PerformanceId);
        }
        var performanceEvent = new PerformanceEvent
        {
            PerformanceId = newEvent.PerformanceId,
            CreatedAt = DateTime.UtcNow,
            StartAt = newEvent.EventDate,
            Performance = performance,
            IsActive = true,
            BuyLink = newEvent.BuyLink
        };
        return await _repository.CreateEventAsync(performanceEvent);
    }

    public async Task<PerformanceEventUpdateDto?> UpdateEvent(PerformanceEventCreateUpdateDto updatedEvent)
    {
        var performance = await _performanceRepository.GetPerformanceById(updatedEvent.PerformanceId);
        if (performance == null)
        {
            throw new PerformanceNotFoundException(updatedEvent.PerformanceId);
        }
        var existingEvent = await _repository.GetByIdAsync(updatedEvent.Id);
        if (existingEvent == null)
        {
            throw new KeyNotFoundException($"PerformanceEvent with id={updatedEvent.Id} not found");
        }

        existingEvent.PerformanceId = updatedEvent.PerformanceId;
        existingEvent.Performance = performance;
        existingEvent.BuyLink = updatedEvent.BuyLink;
        existingEvent.IsActive =  updatedEvent.IsActive;
        
        existingEvent.StartAt = updatedEvent.EventDate.Kind == DateTimeKind.Utc
            ? updatedEvent.EventDate
            : updatedEvent.EventDate.ToUniversalTime();

        return await _repository.UpdateEventAsync(existingEvent);
    }

    public async Task UpdateEventsForPerformance(int performanceId, IEnumerable<AdminPerformanceEventUpdateDto> newEvents)
    {
        var entities = newEvents.Select(e => new PerformanceEvent
        {
            // PerformanceId задаём здесь/в репозитории (клиенту не верим)
            PerformanceId = performanceId,
            StartAt = EnsureUtc(e.StartAt),
            BuyLink = e.BuyLink,
            IsActive = e.IsActive
        }).ToList();

        await _repository.UpdatePerformanceEventsForPerformance(performanceId, entities);
    }

    public async Task<bool> DeleteEvent(int id)
    {
        var existingEvent = await _repository.GetByIdAsync(id);
        if (existingEvent == null)
        {
            throw new KeyNotFoundException($"PerformanceEvent with id={id} not found");
        }

        return await _repository.DeleteAsync(id);
    }
    
    private DateTime EnsureUtc(DateTime dt)
    {
        if (dt.Kind == DateTimeKind.Utc) return dt;

        // если dt пришёл без Kind, чаще всего это локальное время пользователя/браузера
        // НО: тут важно, что ты считаешь источником.
        // Минимально безопасно для Npgsql:
        return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
    }
}