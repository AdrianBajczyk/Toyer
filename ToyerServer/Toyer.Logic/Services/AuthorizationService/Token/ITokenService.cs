using System.Security.Claims;
using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Authorization.Token;

public interface ITokenService
{
    public string GenerateAccessToken(User user, IList<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
