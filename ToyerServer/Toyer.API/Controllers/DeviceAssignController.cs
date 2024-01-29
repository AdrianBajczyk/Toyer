using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Exceptions.FailResponses.Derived.User;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Authorization.AuthorizationHandlers;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class DeviceAssignController(IDeviceAssignmentRepository deviceAssignRepository, IAuthorizationService authorizationService) : ControllerBase
{
    private readonly IDeviceAssignmentRepository _deviceAssignRepository = deviceAssignRepository;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    /// <summary>
    /// Assigns device to user.
    /// </summary>
    [HttpPost("{deviceId}/user/{userId}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignDeviceToUserAsync([FromRoute] string deviceId, [FromRoute] string userId)
    {
        await _deviceAssignRepository.AssignDeviceToUserAsync(deviceId, userId);

        return NoContent();
    }

    /// <summary>
    /// Unssigns device from user.
    /// </summary>
    [HttpDelete("{deviceId}/user/{userId}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignDeviceFromUserAsync([FromRoute] string deviceId, [FromRoute] string userId)
    {
        var ownerId = await _deviceAssignRepository.GetUserIdByAssignedDeviceId(deviceId);
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, ownerId, PermissionRequirements.DeletePermission);

        if (!authorizationResult.Succeeded) throw new AccessException();

        await _deviceAssignRepository.UnassignDeviceFromUserAsync(deviceId, userId);

        return NoContent();
    }
}
