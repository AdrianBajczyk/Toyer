using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Services.Repositories.Interfaces;
using Toyer.Logic.Responses;
using Microsoft.AspNetCore.Authorization;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class DeviceController : ControllerBase
{

    private readonly IDeviceMappings _mappings;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IDeviceAssignRepository _deviceAssignRepository;

    public DeviceController(IDeviceMappings mappings, IDeviceRepository deviceRepository, IDeviceAssignRepository deviceAssignRepository)
    { 
        _mappings = mappings;
        _deviceRepository = deviceRepository;
        _deviceAssignRepository = deviceAssignRepository;
    }

    ///<summary>
    /// Create a new device and enroll it to the Azure IoT hub. 
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateNewDeviceAsync([FromForm]DeviceCreateDto deviceCreateDto)
    {
        var createdDevice = await _deviceRepository.CreateNewDeviceAsync(deviceCreateDto.DeviceTypeId);

        return createdDevice is null
            ? NotFound(new CustomResponse() { Message = "Device type not found.", StatusCode = "404" })
            : CreatedAtAction(nameof(CreateNewDeviceAsync), _mappings.DeviceToDevicePresentDto(createdDevice));
    }

    ///<summary>
    /// Sends chosen order to the device
    /// </summary>
    [HttpPost("{deviceId}/command/{orderId:int}")]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendOrderToDeviceById([FromRoute] string deviceId, [FromRoute] int orderId )
    {
        var response = await _deviceRepository.SendOrderToDevice(deviceId, orderId);

        return response.StatusCode != "200"
            ? NotFound(new CustomResponse() { Message = response.Message, StatusCode = response.StatusCode })
            : Ok(response);
    }

    /// <summary>
    /// Get device by id.
    /// </summary>
    [HttpGet("{deviceId}"), Authorize]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDeviceByIdAsync([FromRoute] string deviceId)
    {
        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);

        return device is null
            ? NotFound(new CustomResponse() { Message = "Device not found.", StatusCode = "404" })
            : Ok(_mappings.DeviceToDevicePresentDto(device));
    }

    /// <summary>
    /// Updates device name by id.
    /// </summary>
    [HttpPut("{deviceId}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatedDeviceNameByIdAsync([FromRoute] string deviceId, [FromForm]DeviceNameUpdateDto nameUpdate)
    {
        var updatedDevice = await _deviceRepository.UpdateDeviceNameAsync(deviceId, nameUpdate.Name);

        return updatedDevice is null
            ? NotFound(new CustomResponse() { Message = "Device not found", StatusCode = "404" })
            : Ok(_mappings.DeviceToDevicePresentDto(updatedDevice));
    }

    /// <summary>
    /// Deletes device by id.
    /// </summary>
    [HttpDelete("{deviceId}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeviceByIdAsync([FromRoute] string deviceId)
    {
        var deletedDevice = await _deviceRepository.DeleteDeviceByIdAsync(deviceId);
        if (deletedDevice != null) await _deviceAssignRepository.DeleteDeviceAsync(deviceId);

        return deletedDevice is null
            ? NotFound(new CustomResponse() { Message = "Device not found.", StatusCode = "404" })
            : Ok(_mappings.DeviceToDevicePresentDto(deletedDevice));
    }
}
