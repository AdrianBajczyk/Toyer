
using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Authorization;

public interface ITokenService
{
    public string CreateToken(User user);
}
