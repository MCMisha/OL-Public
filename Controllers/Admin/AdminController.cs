using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models.DTO;
using WebApplicationOperaLublin.Options;

namespace WebApplicationOperaLublin.Controllers.Admin;

[ApiController]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    
    private readonly IAdminService _adminService;

    public AdminController(ILogger<AdminController> logger, IAdminService adminService)
    {
        _logger = logger;
        _adminService = adminService;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        _logger.LogInformation("Login attempt");

        var tokens = await _adminService.LoginAsync(request.Login, request.Password);

        if (tokens == null)
        {
            return Unauthorized("Invalid login or password.");
        }

        SetAuthCookies(tokens);

        return Ok(new { IsAuthenticated = true });
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refresh_token"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized("Refresh token not found.");
        }

        var tokens = await _adminService.RefreshTokenAsync(refreshToken);

        if (tokens == null)
        {
            DeleteAuthCookies();
            return Unauthorized("Invalid refresh token.");
        }

        SetAuthCookies(tokens);

        return Ok(new { IsAuthenticated = true });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refresh_token"];

        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _adminService.LogoutAsync(refreshToken);
        }

        DeleteAuthCookies();

        return Ok();
    }

    [Authorize]
    [HttpGet("verify")]
    public IActionResult VerifyToken()
    {
        return Ok(new
        {
            IsValid = true,
            Username = User.Identity?.Name
        });
    }

    private void SetAuthCookies(AuthTokensDto tokens)
    {
        var accessCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddMinutes(AuthOptions.ExpiresMinutes)
        };

        var refreshCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("jwt_token", tokens.AccessToken, accessCookieOptions);
        Response.Cookies.Append("refresh_token", tokens.RefreshToken, refreshCookieOptions);
    }

    private void DeleteAuthCookies()
    {
        Response.Cookies.Delete("jwt_token", new CookieOptions
        {
            Secure = true,
            SameSite = SameSiteMode.None
        });

        Response.Cookies.Delete("refresh_token", new CookieOptions
        {
            Secure = true,
            SameSite = SameSiteMode.None
        });
    }
}