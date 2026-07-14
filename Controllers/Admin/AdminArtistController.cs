using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AdminArtistController : Controller
{
    private readonly ILogger<AdminArtistController> _logger;
    private readonly IAdminArtistsService _adminArtistsService;

    public AdminArtistController(ILogger<AdminArtistController> logger, IAdminArtistsService adminArtistsService)
    {
        _logger = logger;
        _adminArtistsService = adminArtistsService;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var artists = await _adminArtistsService.GetArtists();
        return Ok(artists);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateArtist([FromBody] ArtistCreateUpdateDto artistCreateUpdateDto)
    {
        var createdArtist = await _adminArtistsService.CreateArtistAsync(artistCreateUpdateDto);
        if (createdArtist == null)
        {
            return BadRequest();
        }
        return Ok(createdArtist);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtist(int id)
    {
        var artist = await _adminArtistsService.GetArtistById(id);
        if (artist == null)
        {
            return NotFound();
        }
        return Ok(artist);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateArtist([FromBody] ArtistCreateUpdateDto artistCreateUpdateDto)
    {
        var updatedArtist = await _adminArtistsService.UpdateArtist(artistCreateUpdateDto);
        if (updatedArtist == null)
        {
            return BadRequest();
        }

        return Ok(updatedArtist);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteArtist(int id)
    {
        var result = await _adminArtistsService.DeleteArtist(id);
        if (result == false)
        {
            return NotFound();
        }
        return Ok(result);
    }
}