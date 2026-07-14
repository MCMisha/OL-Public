using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;

namespace WebApplicationOperaLublin.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtistPhotoController : ControllerBase
{
    private readonly IArtistPhotoService _artistPhotoService;

    public ArtistPhotoController(IArtistPhotoService artistPhotoService)
    {
        _artistPhotoService = artistPhotoService;
    }

    [HttpGet("by-artist/{artistId:int}")]
    public async Task<IActionResult> GetArtistPhotos(int artistId)
    {
        var photos = await _artistPhotoService.GetArtistPhotosByArtistIdAsync(artistId);

        return Ok(photos);
    }
}