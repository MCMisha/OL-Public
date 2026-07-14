using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByLogin(string login) => await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

    public async Task UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    
    public async Task<User?> GetUserByRefreshTokenHash(string refreshTokenHash)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.RefreshTokenHash == refreshTokenHash);
    }
}