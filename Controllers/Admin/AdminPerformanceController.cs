using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminPerformanceController : Controller
{
    private readonly IAdminPerformanceService _adminPerformanceService;
    public AdminPerformanceController(IAdminPerformanceService adminPerformanceService)
    {
        _adminPerformanceService = adminPerformanceService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Performance>>> Get()
    {
        var performances = await _adminPerformanceService.GetPerformances();
        return Ok(performances);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Performance>> GetById(int id)
    {
        var performance = await _adminPerformanceService.GetPerformanceById(id);
        if (performance == null)
        {
            return NotFound();
        }

        return Ok(performance);
    }

    [HttpPost("new")]
    public async Task<ActionResult<Performance>> Create([FromBody] PerformanceCreateUpdateDto performance)
    {
        var createdPerformance = await _adminPerformanceService.CreatePerformanceAsync(performance);
        if (createdPerformance == null)
        {
            return BadRequest();
        }
        return Ok(createdPerformance);
    }

    [HttpPut("update")]
    public async Task<ActionResult<Performance>> Update([FromBody] PerformanceCreateUpdateDto updatedPerformance)
    {
        var updatedPerformanceResult = await _adminPerformanceService.UpdatePerformance(updatedPerformance);
        if (updatedPerformanceResult == null)
        {
            return BadRequest();
        }

        return Ok(updatedPerformanceResult);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var deleted = await _adminPerformanceService.DeletePerformance(id);
        return Ok(deleted);
    }
}