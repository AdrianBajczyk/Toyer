namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface ITokenRepository
{
    Task<(string accessToken, string refreshToken)> RefreshAsyc(string accessToken, string refreshToken);
    Task RevokeAsync();

}