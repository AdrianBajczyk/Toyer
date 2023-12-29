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
    public async Task<IActionResult> CreateNewDeviceTypeAsync([FromForm] DeviceTypeCreateDto newDeviceType)
    {
        var createdDeviceType = await _deviceTypeRepository.CreateDeviceTypeAsync(_mappings.DeviceTypeCreateDtoToDeviceType(newDeviceType));
        var result = _mappings.DeviceTypeToDeviceTypePresentDto(createdDeviceType!);

        return CreatedAtAction(nameof(CreateNewDeviceTypeAsync), result);
    }

    /// <summary>
    /// Gets info of speciffic device type.
    /// </summary>
    [HttpGet("{deviceTypeId:int}")]
    [ProducesResponseType(typeof(DeviceTypePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDeviceTypeByIdAsync([FromRoute] int deviceTypeId)
    {
        var deviceType = await _deviceTypeRepository.GetDeviceTypeByIdAsync(deviceTypeId);

        return deviceType is null
            ? NotFound(new ErrorDetails { Message = "Device type not found", StatusCode = 404 })
            : Ok(_mappings.DeviceTypeToDeviceTypePresentDto(deviceType));
    }

    /// <summary>
    /// Gets info of all devices.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(DeviceTypePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllDeviceTypesAsync()
    {
        var deviceTypes = await _deviceTypeRepository.GetAllDeviceTypesAsync();

        return deviceTypes is null
            ? NotFound(new ErrorDetails { Message = "Device type not found", StatusCode = 404 })
            : Ok(_mappings.DeviceTypesToDeviceTypePresentDtos(deviceTypes));
    }

    ///<summary>
    /// Updates name of device type info by id.
    /// </summary>
    [HttpPut("{deviceTypeId:int}")]
    [ProducesResponseType(typeof(DeviceTypePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDeviceTypeInfoAsync([FromRoute]int deviceTypeId, [FromForm]DeviceTypeCreateDto deviceUpdateDto)
    {
        var updatedDeviceType = await _deviceTypeRepository.UpdateDeviceTypeAsync(deviceTypeId, _mappings.DeviceTypeCreateDtoToDeviceType(deviceUpdateDto));

        return updatedDeviceType is null
            ? NotFound(new ErrorDetails { Message = "Device type not found", StatusCode = 404 })
            : Ok(_mappings.DeviceTypeToDeviceTypePresentDto(updatedDeviceType));
    }

    ///<summary>
    /// Deletes device type selected by id.
    /// </summary>
    [HttpDelete("{deviceTypeId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeviceTypeByIdAsync([FromRoute] int deviceTypeId)
    {
        var deletedDevice = await _deviceTypeRepository.DeleteDeviceTypeAsync(deviceTypeId);

        return deletedDevice is null
            ? NotFound(new ErrorDetails { Message = "Device type not found", StatusCode = 404 })
            : Ok();
    }
}
