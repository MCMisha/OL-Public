using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminMainBackgroundController : ControllerBase
{
    private readonly IAdminMainPageBackgroundService _adminMainPageBackgroundService;

    public AdminMainBackgroundController(IAdminMainPageBackgroundService adminMainPageBackgroundService)
    {
        _adminMainPageBackgroundService = adminMainPageBackgroundService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _adminMainPageBackgroundService.GetMainPageBackgroundsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var mainPageBackground = await _adminMainPageBackgroundService.GetMainPageBackgroundByIdAsync(id);
        if (mainPageBackground == null)
        {
            return NotFound($"Background with id {id} not found.");
        }
        return Ok(mainPageBackground);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateMainPageBackground([FromBody] CreateMainPageBackgroundDto request)
    {
        if (string.IsNullOrWhiteSpace(request.MainImage))
        {
            return BadRequest("ImageBase64 is required.");
        }
        
        var model = new MainPageBackground
        {
            PerformanceId = request.PerformanceId,
            MainImage = string.IsNullOrEmpty(request.MainImage) ? null! : Convert.FromBase64String(request.MainImage),
            IsActive = request.IsActive,
            DisplayOrder = request.DisplayOrder
        };
        return Ok(await _adminMainPageBackgroundService.CreateMainPageBackgroundAsync(model));
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateMainPageBackground([FromBody] UpdateMainPageBackgroundDto request, int id)
    {
        if (id != request.Id)
        {
            return BadRequest("Route id does not match body id.");
        }

        var existing = await _adminMainPageBackgroundService.GetMainPageBackgroundByIdAsync(id);
        if (existing == null)
        {
            return NotFound($"Background with id {id} not found.");
        }
        var updatedModel = new MainPageBackground
        {
            Id = request.Id,
            PerformanceId = request.PerformanceId,
            MainImage = string.IsNullOrEmpty(request.MainImage) ? null! : Convert.FromBase64String(request.MainImage),
            IsActive = request.IsActive,
            DisplayOrder = request.DisplayOrder
        };
        return Ok(await _adminMainPageBackgroundService.UpdateMainPageBackgroundAsync(updatedModel));
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteMainPageBackground(int id)
    {
        var result = await _adminMainPageBackgroundService.DeleteMainPageBackgroundAsync(id);
        if (!result)
        {
            return NotFound($"Background with id {id} not found.");
        }
        return Ok(result);
    }

}