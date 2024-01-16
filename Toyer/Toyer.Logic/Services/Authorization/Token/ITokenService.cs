using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Authorization.Token;

public interface ITokenService
{
    public string CreateToken(User user, string role);
}
