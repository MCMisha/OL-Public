using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminPerformanceInfoController : Controller
{
    private readonly IAdminPerformanceInfoService _adminPerformanceInfoService;

    public AdminPerformanceInfoController(IAdminPerformanceInfoService adminPerformanceInfoService)
    {
        _adminPerformanceInfoService = adminPerformanceInfoService;
    }

    [HttpPost("{performanceId:int}/implementers")]
    public async Task<IActionResult> AddImplementers(
        [FromRoute] int performanceId,
        [FromBody] ImplementersRequest? implementers)
    {
        if (implementers == null)
        {
            return BadRequest("Implementers payload is null.");
        }

        var result = await _adminPerformanceInfoService.AddImplementers(implementers.Implementers, performanceId);
        return Ok(result);
    }

    [HttpGet("{performanceId:int}/implementers")]
    public async Task<IActionResult> GetImplementersByPerformance([FromRoute] int performanceId)
    {
        var result = await _adminPerformanceInfoService.GetImplementersByPerformance(performanceId);
        return Ok(result);
    }

    [HttpGet("{performanceId:int}/implementers/{implementerId:int}")]
    public async Task<IActionResult> GetImplementerById(
        [FromRoute] int performanceId,
        [FromRoute] int implementerId)
    {
        var result = await _adminPerformanceInfoService.GetImplementerById(performanceId, implementerId);
        if (result == null)
        {
            return NotFound($"Implementer {implementerId} for performance {performanceId} was not found.");
        }

        return Ok(result);
    }

    [HttpPut("{performanceId:int}/implementers/{implementerId:int}")]
    public async Task<IActionResult> UpdateImplementer([FromBody] ImplementerCreateUpdateDto implementer,
        [FromRoute] int performanceId, [FromRoute] int implementerId)
    {
        if (implementer == null)
        {
            return BadRequest("Implementer payload is null.");
        }

        var result = await _adminPerformanceInfoService.UpdateImplementer(performanceId, implementerId, implementer);
        return Ok(result);
    }

    [HttpDelete("{performanceId:int}/implementers/{implementerId:int}")]
    public async Task<IActionResult> DeleteImplementer(
        [FromRoute] int performanceId,
        [FromRoute] int implementerId)
    {
        var deleted = await _adminPerformanceInfoService.DeleteImplementer(performanceId, implementerId);
        if (!deleted)
        {
            return NotFound($"Implementer {implementerId} for performance {performanceId} was not found.");
        }

        return Ok(deleted);
    }
}