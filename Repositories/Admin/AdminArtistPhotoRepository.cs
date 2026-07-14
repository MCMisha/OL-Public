using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminArtistPhotoRepository: GenericRepository<ArtistPhoto>, IAdminArtistPhotoRepository
{
    private readonly AppDbContext _context;

    public AdminArtistPhotoRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ArtistPhoto>> GetArtistPhotosByArtistIdAsync(int artistId)
    {
        return await _context.ArtistPhotos
            .Where(photo => photo.ArtistId == artistId)
            .ToListAsync();
    }

    public async Task AddPhotosAsync(IEnumerable<ArtistPhoto> artistPhotos)
    {
        await _context.ArtistPhotos.AddRangeAsync(artistPhotos);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePhotosByArtistIdAsync(int artistId)
    {
        var photos = await _context.ArtistPhotos
            .Where(photo => photo.ArtistId == artistId)
            .ToListAsync();

        if (!photos.Any())
        {
            return;
        }

        _context.ArtistPhotos.RemoveRange(photos);
        await _context.SaveChangesAsync();
    }
}