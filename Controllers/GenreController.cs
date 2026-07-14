using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : Controller
{
    private readonly IGenreService _service;
    public GenreController(IGenreService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Genre>>> GetGenres()
    {
        try
        {
            var genres = await _service.GetGenres();
            if (!genres.Any())
            {
                return NotFound("No genres found.");
            }
            return Ok(genres);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}