using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories.Admin;

public class AdminPublicCommentRepository : GenericRepository<PublicComment>, IAdminPublicCommentRepository
{
    public AdminPublicCommentRepository(AppDbContext context) : base(context) { }
    
}