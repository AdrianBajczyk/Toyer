﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;
using Toyer.Data.Context;
using Toyer.Logic.Services.Authorization.Token;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlTokenRepository(ITokenService tokenService, UsersDbContext usersDbContext, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : ITokenRepository
{
    private readonly ITokenService _tokenService = tokenService;
    private readonly UsersDbContext _usersDbContext = usersDbContext;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<(string accessToken, string refreshToken)> RefreshAsyc(string accessToken, string refreshToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        var userId = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var userTokens = await _usersDbContext.RefreshTokens.SingleOrDefaultAsync(t => t.UserId == userId);
        if (userTokens is null || userTokens.RefreshToken != refreshToken || userTokens.RefreshTokenExpiryTime <= DateTime.Now) throw new AuthenticationException();

        var user = await _userRepository.GetUserByIdAsync(userId);
        var role = principal.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value).ToList() ?? throw new AuthenticationException();

        var newAccessToken = _tokenService.GenerateAccessToken(user, role);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokenModel!.RefreshToken = newRefreshToken;
        _usersDbContext.SaveChanges();

        return (newAccessToken, newRefreshToken);
    }

    public async Task RevokeAsync()
    {
        var refreshTokenCookie = _httpContextAccessor.HttpContext?.Request?.Cookies["refreshToken"] ?? throw new AuthenticationException();

        var refreshToken = await _usersDbContext.RefreshTokens.SingleOrDefaultAsync(t => t.RefreshToken == refreshTokenCookie) ?? throw new AuthenticationException();
        _usersDbContext.RefreshTokens.Remove(refreshToken);
        await _usersDbContext.SaveChangesAsync();

        return;
    }
}
