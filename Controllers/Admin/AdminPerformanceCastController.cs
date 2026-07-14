using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Exceptions;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]/{performanceId}/[action]")]
public class AdminPerformanceCastController : ControllerBase
{
    private readonly IAdminCastService _service;
    private readonly IAdminPerformanceService _performanceService;
    public AdminPerformanceCastController(IAdminCastService service, IAdminPerformanceService performanceService)
    {
        _service = service;
        _performanceService = performanceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCast(int performanceId)
    {
        var performance = await _performanceService.GetPerformanceById(performanceId);
        if (performance == null)
        {
            throw new PerformanceNotFoundException(performanceId);
        }
        var cast = await _service.GetCast(performanceId);
        return Ok(await _service.GetCast(performanceId));
    }

    [HttpPost]
    public async Task<IActionResult> AddCast(
        int performanceId,
        [FromBody] List<CastCreateDto> cast)
    {
        var performanceById = await _performanceService.GetPerformanceById(performanceId);
        if (performanceById == null)
        {
            return BadRequest($"Performance by id ({performanceId}) wasn't found.");
        }
        
        if (cast.Count == 0)
        {
            return BadRequest("Cast is empty.");
        }
        
        if (cast.Any(x => string.IsNullOrWhiteSpace(x.Role)))
        {
            return BadRequest("Role is required for each cast member.");
        }

        await _service.AddCast(cast);
        return Ok();
    }

    [HttpDelete("{artistId}")]
    public async Task<IActionResult> DeleteCast(int performanceId, int eventId, int artistId)
    {
        var result = await _service.RemoveCast(performanceId, eventId, artistId);
        return result ? Ok() : NotFound();
    }
}