using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class PlaceRepository : IPlaceRepository
{
    private readonly AppDbContext _context;
    public PlaceRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Place?> GetPlaceById(int id) => await _context.Places.FirstOrDefaultAsync(place => place.Id == id);

    public async Task<IEnumerable<Place>> GetPlaces() => await _context.Places.ToListAsync();
}