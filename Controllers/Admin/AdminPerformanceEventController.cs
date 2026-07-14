using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminPerformanceEventController : ControllerBase
{
    private readonly IAdminPerformanceEventService _performanceEventService;

    public AdminPerformanceEventController(IAdminPerformanceEventService performanceEventService)
    {
        _performanceEventService = performanceEventService;
    }

    [HttpPost("new")]
    public async Task<IActionResult> CreateNewPerformanceEvent(PerformanceEventCreateUpdateDto performanceEventCreateUpdateDto)
    {
        AdminPerformanceEventListDto? newEvent = await _performanceEventService.CreateEvent(performanceEventCreateUpdateDto);
        if (newEvent == null)
        {
            return BadRequest();
        }

        return Ok("Successfully created new performance event");
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPerformanceEvents()
    {
        IEnumerable<AdminPerformanceEventListDto> performanceEvents = await _performanceEventService.GetAllEvents();
        return Ok(performanceEvents);
    }

    [HttpGet("by-performance/{performanceId}")]
    public async Task<IActionResult> GetPerformanceEventByPerformanceId(int performanceId)
    {
        IEnumerable<AdminPerformanceEventListDto> performanceEvents =
            await _performanceEventService.GetByPerformanceId(performanceId);
        return Ok(performanceEvents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerformanceEventById(int id)
    {
        PerformanceEventDto? performanceEvent = await _performanceEventService.GetEventById(id);
        if (performanceEvent == null)
        {
            throw new PerformanceEventNotFoundException(id);
        }

        return Ok(performanceEvent);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePerformanceEvent(PerformanceEventCreateUpdateDto performanceEventCreateUpdateDto)
    {
        PerformanceEventUpdateDto? updatedEvent = await _performanceEventService.UpdateEvent(performanceEventCreateUpdateDto);
        if (updatedEvent == null)
        {
            return BadRequest();
        }

        return Ok("Successfully updated performance event");
    }

    [HttpPut("performance/{performanceId}/update")]
    public async Task<IActionResult> UpdatePerformanceEventsForPerformance(
        [FromRoute] int performanceId,
        [FromBody] List<AdminPerformanceEventUpdateDto> events)
    {
        if (events is null)
        {
            return BadRequest("Body is required");
        }

        await _performanceEventService.UpdateEventsForPerformance(performanceId, events);
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePerformanceEvent(int id)
    {
        bool result = await _performanceEventService.DeleteEvent(id);
        if (!result)
        {
            return BadRequest();
        }

        return Ok("Successfully deleted performance event");
    }
}