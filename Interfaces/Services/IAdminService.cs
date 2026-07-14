using WebApplicationOperaLublin.Models.DTO;

namespace WebApplicationOperaLublin.Interfaces.Services;

public interface IAdminService
{
    Task<AuthTokensDto?> LoginAsync(string login, string password);
    Task<AuthTokensDto?> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(string refreshToken);
}