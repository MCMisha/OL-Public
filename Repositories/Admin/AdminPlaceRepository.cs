using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminPlaceRepository : GenericRepository<Place>, IAdminPlaceRepository
{
    public AdminPlaceRepository(AppDbContext context) : base(context)
    {
    }
}