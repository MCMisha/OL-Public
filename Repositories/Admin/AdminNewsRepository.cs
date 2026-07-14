using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminNewsRepository : GenericRepository<News>, IAdminNewsRepository
{
    public AdminNewsRepository(AppDbContext context) : base(context) { }
}