using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtistController : Controller
{
    private readonly ILogger<ArtistController> _logger;
    private readonly IArtistsService _artistsService;

    public ArtistController(ILogger<ArtistController> logger, IArtistsService artistsService)
    {
        _logger = logger;
        _artistsService = artistsService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var artists = await _artistsService.GetArtists();
        return Ok(artists);
    }

    [HttpGet("all-main")]
    public async Task<IActionResult> GetAllMain()
    {
        var artists = await _artistsService.GetArtistsMain();
        return Ok(artists);
    }
    
    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetArtist(int id)
    {
        var artist = await _artistsService.GetArtistById(id);
        if (artist == null)
        {
            return NotFound();
        }
        return Ok(artist);
    }
}