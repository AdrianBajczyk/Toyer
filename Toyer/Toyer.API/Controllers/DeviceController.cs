using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Services.Repositories.Interfaces;
using Toyer.Logic.Dtos.DeviceType;
using Toyer.Logic.Responses;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class DeviceController : ControllerBase
{

    private readonly IDeviceMappings _mappings;
    private readonly IDeviceRepository _deviceRepository;

    public DeviceController(IDeviceMappings mappings, IDeviceRepository deviceRepository)
    { 
        _mappings = mappings;
        _deviceRepository = deviceRepository;
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
            ? NotFound(new CustomResponse() { Message = "Device type not found.", StatusCode = 404})
            : CreatedAtAction(nameof(CreateNewDeviceAsync), _mappings.DeviceToDevicePresentDto(createdDevice));
    }

    ///<summary>
    /// Sends chosen order to the device
    /// </summary>
    [HttpPost("{deviceId:guid}/command/{orderId:int}")]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendOrderToDeviceById([FromRoute] Guid deviceId, [FromRoute] int orderId )
    {
        var response = await _deviceRepository.SendOrderToDevice(deviceId, orderId);

        return response.StatusCode != 202
            ? NotFound(new CustomResponse() { Message = response.Message, StatusCode = response.StatusCode })
            : Accepted(response);
    }

    /// <summary>
    /// Get device by id.
    /// </summary>
    [HttpGet("{deviceId:Guid}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDeviceByIdAsync([FromRoute] Guid deviceId)
    {
        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);

        return device is null
            ? NotFound(new CustomResponse() { Message = "Device not found.", StatusCode = 404})
            : Ok(_mappings.DeviceToDevicePresentDto(device));
    }

    /// <summary>
    /// Updates device name by id.
    /// </summary>
    [HttpPut("{deviceId:Guid}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatedDeviceNameByIdAsync([FromRoute] Guid deviceId, [FromForm]DeviceNameUpdateDto nameUpdate)
    {
        var updatedDevice = await _deviceRepository.UpdateDeviceNameAsync(deviceId, nameUpdate.Name);

        return updatedDevice is null
            ? NotFound(new CustomResponse() { Message = "Device not found", StatusCode = 404})
            : Ok(_mappings.DeviceToDevicePresentDto(updatedDevice));
    }

    /// <summary>
    /// Deletes device by id.
    /// </summary>
    [HttpDelete("{deviceId:guid}")]
    [ProducesResponseType(typeof(DevicePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeviceByIdAsync([FromRoute] Guid deviceId)
    {
        var deletedDevice = await _deviceRepository.DeleteDeviceByIdAsync(deviceId);

        return deletedDevice is null
            ? NotFound(new CustomResponse() { Message = "Device not found.", StatusCode = 404})
            : Ok(_mappings.DeviceToDevicePresentDto(deletedDevice));
    }
}
