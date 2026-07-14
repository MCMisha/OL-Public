using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using WebApplicationOperaLublin.Helpers;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Options;

namespace WebApplicationOperaLublin.Services;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AdminService> _logger;

    public AdminService(IUserRepository userRepository, ILogger<AdminService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<AuthTokensDto?> LoginAsync(string login, string password)
    {
        _logger.LogInformation("Login attempt for user: {Login}", login);

        var user = await _userRepository.GetUserByLogin(login);

        if (user == null)
        {
            return null;
        }

        if (user.LastLoginAttemptTime?.AddMinutes(30) < DateTime.UtcNow)
        {
            user.LoginAttempt = 0;
        }

        if (user.LoginAttempt >= 3)
        {
            return null;
        }

        if (!Sha256HashHelper.GetHash(password).Equals(user.PasswordHash))
        {
            user.LoginAttempt++;
            user.LastLoginAttemptTime = DateTime.UtcNow;
            await _userRepository.UpdateUser(user);
            return null;
        }

        user.LoginAttempt = 0;
        user.LastLoginAttemptTime = null;

        var tokens = CreateTokens(user.Login);

        user.RefreshTokenHash = Sha256HashHelper.GetHash(tokens.RefreshToken);
        user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

        await _userRepository.UpdateUser(user);

        return tokens;
    }

    public async Task<AuthTokensDto?> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return null;
        }

        var refreshTokenHash = Sha256HashHelper.GetHash(refreshToken);
        var user = await _userRepository.GetUserByRefreshTokenHash(refreshTokenHash);

        if (user == null || user.RefreshTokenExpiresAt <= DateTime.UtcNow)
        {
            return null;
        }

        var tokens = CreateTokens(user.Login);

        user.RefreshTokenHash = Sha256HashHelper.GetHash(tokens.RefreshToken);
        user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

        await _userRepository.UpdateUser(user);

        return tokens;
    }

    public async Task LogoutAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return;
        }

        var refreshTokenHash = Sha256HashHelper.GetHash(refreshToken);
        var user = await _userRepository.GetUserByRefreshTokenHash(refreshTokenHash);

        if (user == null)
        {
            return;
        }

        user.RefreshTokenHash = null;
        user.RefreshTokenExpiresAt = null;

        await _userRepository.UpdateUser(user);
    }

    private AuthTokensDto CreateTokens(string login)
    {
        var accessToken = CreateAccessToken(login);
        var refreshToken = CreateRefreshToken();

        return new AuthTokensDto { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    private string CreateAccessToken(string login)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, login)
        };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(AuthOptions.ExpiresMinutes),
            signingCredentials: new SigningCredentials(
                AuthOptions.SymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private static string CreateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}