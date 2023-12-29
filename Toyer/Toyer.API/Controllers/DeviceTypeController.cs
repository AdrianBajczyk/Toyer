using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.DeviceType;
using Toyer.Logic.Exceptions;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class DeviceTypeController : ControllerBase
{
    private readonly IDeviceTypeMappings _mappings;
    private readonly IDeviceTypeRepository _deviceTypeRepository;

    public DeviceTypeController(IDeviceTypeRepository deviceTypeRepository, IDeviceTypeMappings mappings)
    {
        _deviceTypeRepository = deviceTypeRepository;
        _mappings = mappings;
    }

    /// <summary>
    /// Creates new device type which ultimately is container for availble orders for speiffic device
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DeviceTypePresentDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewDeviceType([FromForm] DeviceTypeCreateDto newDeviceType)
    {
        var createDeviceType = await _deviceTypeRepository.CreateDeviceTypeAsync(_mappings.DeviceTypeCreateDtoToDeviceType(newDeviceType));

        return CreatedAtAction(nameof(CreateNewDeviceType), _mappings.DeviceTypeToDeviceTypePresentDto(createDeviceType));
    }

    /// <summary>
    /// Gets info of speciffic device type.
    /// </summary>
    [HttpGet("{deviceTypeId:int}")]
    [ProducesResponseType(typeof(DeviceTypePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetShortById([FromRoute] int deviceTypeId)
    {
        var deviceType = await _deviceTypeRepository.GetDeviceTypeByIdAsync(deviceTypeId);

        return deviceType is null
            ? NotFound(new ErrorDetails { Message = "Device type not found" })
            : Ok(_mappings.DeviceTypeToDeviceTypePresentDto(deviceType));
    }
}
