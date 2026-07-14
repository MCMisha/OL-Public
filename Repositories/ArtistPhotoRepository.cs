using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class ArtistPhotoRepository : IArtistPhotoRepository
{
    private readonly AppDbContext _context;

    public ArtistPhotoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ArtistPhoto>> GetArtistPhotosByArtistIdAsync(int artistId)
    {
        return await _context.ArtistPhotos
            .Where(photo => photo.ArtistId == artistId)
            .ToListAsync();
    }
}
