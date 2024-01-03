using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Services.Repositories.Interfaces;
using Toyer.Logic.Exceptions;

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


}
