using WebApplicationOperaLublin.Models;
namespace WebApplicationOperaLublin.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByLogin(string login);
    Task UpdateUser(User user);
    Task<User?> GetUserByRefreshTokenHash(string refreshTokenHash);
}