using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApplicationOperaLublin.Options;

public class AuthOptions
{
    private static string _issuer = null!;
    private static string _audience = null!;
    private static string _key = null!;
    private static int _expiresMinutes;

    private AuthOptions(IConfiguration configuration)
    {
        _issuer = configuration["AuthConfigurations:Issuer"] ?? string.Empty;
        _audience = configuration["AuthConfigurations:Audience"] ?? string.Empty;
        _key = configuration["AuthConfigurations:Key"] ?? string.Empty;
        _expiresMinutes = int.TryParse(configuration["AuthConfigurations:ExpiresMinutes"], out var value) ? value : 0;
    }
    
    public static void Create(IConfiguration configuration)
    {
        _ = new AuthOptions(configuration);
    }

    public static string Issuer => _issuer;
    public static string Audience => _audience;
    public static SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.UTF8.GetBytes(_key));
    public static int ExpiresMinutes => _expiresMinutes;
}
