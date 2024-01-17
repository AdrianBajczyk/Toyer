using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Toyer.Data.Context;
using Toyer.Logic.Dtos.Token;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class TokenController(UsersDbContext usersDbContext, IUserRepository userRepository, ITokenRepository tokenRepository) : ControllerBase
{
    private readonly UsersDbContext _usersDbContext = usersDbContext;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenRepository _tokenRepository = tokenRepository;

    /// <summary>
    /// Refresh users tokens.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenApiDto)
    {
        var (accessToken, refreshToken) = await _tokenRepository.RefreshAsyc(tokenApiDto.AccessToken, tokenApiDto.RefreshToken);

        return Ok(new AuthenticationResponse()
        {
            Message = "Success.",
            StatusCode = 200,
            Token = accessToken,
            RefreshToken = refreshToken
        });
    }

    /// <summary>
    /// Invalidates tokens.
    /// </summary>
    [HttpPost]
    [Route("End")]
    public async Task<IActionResult> Revoke()
    {
        await _tokenRepository.RevokeAsync();

        return NoContent();
    }
}
