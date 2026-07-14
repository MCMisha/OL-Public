using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminPlaceController : Controller
{
    private IAdminPlaceService _service;
    private IAdminPerformanceService _performanceService;

    public AdminPlaceController(IAdminPlaceService service, IAdminPerformanceService performanceService)
    {
        _service = service;
        _performanceService = performanceService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var place = await _service.GetPlaceById(id);
        if (place == null)
        {
            return NotFound("Place not found");
        }

        return Ok(place);
    }

    [HttpGet("all")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetPlaces());
    }

    [HttpPost("new")]
    public async Task<IActionResult> CreatePlace([FromBody] Place place)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPlace = await _service.CreatePlaceAsync(place);
        if (createdPlace == null)
        {
            return StatusCode(500, "An error occurred while creating the place");
        }

        return Ok(createdPlace);
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdatePlace(int id, [FromBody] Place place)
    {
        if (place.Id != id)
        {
            return BadRequest("Invalid place data");
        }

        var updatedPlace = await _service.UpdatePlace(place);
        if (updatedPlace == null)
        {
            return NotFound("Place not found");
        }

        return Ok(updatedPlace);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeletePlace(int id)
    {
        var existingPlace = await _service.GetPlaceById(id);
        if (existingPlace == null)
        {
            return NotFound("Place not found");
        }

        if (await _performanceService.ExistsPerformancesByPlace(existingPlace))
        {
            return BadRequest("This place has performances");
        }

        if (!await _service.DeletePlace(id))
        {
            return NotFound("Place not found");
        }

        return Ok();
    }
}