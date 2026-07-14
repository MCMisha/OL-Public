using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminGenreController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly IAdminGenreService _adminGenreService;
    private readonly IAdminPerformanceService _adminPerformanceService;

    public AdminGenreController(ILogger<AdminController> logger, IAdminGenreService genreService,
        IAdminPerformanceService performanceService)
    {
        _logger = logger;
        _adminGenreService = genreService;
        _adminPerformanceService = performanceService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetById(int id)
    {
        var genre = await _adminGenreService.GetGenreById(id);
        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Genre>>> Get()
    {
        var genres = await _adminGenreService.GetGenres();
        return Ok(genres);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Genre?>> Create([FromBody] Genre? genre)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var genreNew = await _adminGenreService.CreateGenreAsync(genre);
        if (genreNew == null)
        {
            return BadRequest("Genre cannot be null");
        }

        return genreNew;
    }

    [HttpPut("update")]
    public async Task<ActionResult<Genre>> Update([FromBody] Genre genre)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updateGenre = await _adminGenreService.UpdateGenre(genre);

        if (updateGenre == null)
        {
            return BadRequest("Genre cannot be null");
        }

        return Ok(updateGenre);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var existingGenre = await _adminGenreService.GetGenreById(id);
        if (existingGenre == null)
        {
            return NotFound();
        }

        if (await _adminPerformanceService.ExistsPerformancesByGenre(existingGenre))
        {
            return BadRequest("This genre has performances");
        }

        if (!await _adminGenreService.DeleteGenre(id))
        {
            return NotFound();
        }

        return Ok();
    }
}