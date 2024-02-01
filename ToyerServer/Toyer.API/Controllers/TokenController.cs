using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyer.Data.Context;
using Toyer.Logic.Dtos.Token;
using Toyer.Logic.Exceptions.FailResponses.Abstract;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class TokenController(ITokenRepository tokenRepository) : ControllerBase
{
    private readonly ITokenRepository _tokenRepository = tokenRepository;

    /// <summary>
    /// Refresh users tokens.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Refresh([FromBody]TokenDto tokenApiDto)
    {
        string refreshToken = HttpContext.Request.Cookies["refreshToken"] ?? throw new ForbiddenException();
            
        var (accessToken, newRefreshToken) = await _tokenRepository.RefreshAsyc(tokenApiDto.AccessToken, refreshToken);

        Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, 
        });

        return Ok(new AuthenticationResponse()
        {
            Message = "Success.",
            Status = 200,
            Token = accessToken,
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
