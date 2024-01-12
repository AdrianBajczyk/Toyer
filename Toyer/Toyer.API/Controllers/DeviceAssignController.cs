using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class DeviceAssignController : ControllerBase
{
    private readonly IDeviceAssignRepository _deviceAssignRepository;

    public DeviceAssignController(IDeviceAssignRepository deviceAssignRepository)
    {
        _deviceAssignRepository = deviceAssignRepository;
    }

    /// <summary>
    /// Assigns device to user.
    /// </summary>
    [HttpPost("{deviceId}/user/{userId}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignDeviceToUserAsync([FromRoute] string deviceId, [FromRoute] string userId)
    {
        var assignDeviceResult = await _deviceAssignRepository.AssignDeviceToUserAsync(deviceId, userId);

        return new ObjectResult(assignDeviceResult);
    }

    /// <summary>
    /// Unssigns device from user.
    /// </summary>
    [HttpDelete("{deviceId}/user/{userId}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignDeviceFromUserAsync([FromRoute] string deviceId, [FromRoute] string userId)
    {
        var unassignDeviceResult = await _deviceAssignRepository.UnassignDeviceFromUserAsync(deviceId, userId);

        return new ObjectResult(unassignDeviceResult);
    }
}
