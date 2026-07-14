using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminGenreRepository : GenericRepository<Genre>, IAdminGenreRepository
{
    public AdminGenreRepository(AppDbContext context) : base(context)
    {
    }
}