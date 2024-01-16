using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.DeviceType;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class DeviceTypeController(IDeviceTypeRepository deviceTypeRepository, IDeviceTypeMappings mappings) : ControllerBase
{
    private readonly IDeviceTypeMappings _mappings = mappings;
    private readonly IDeviceTypeRepository _deviceTypeRepository = deviceTypeRepository;

    /// <summary>
    /// Creates new device type which ultimately is container for availble orders for speiffic device
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "Production")]
    [ProducesResponseType(typeof(DeviceTypePresentDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewDeviceTypeAsync([FromForm] DeviceTypeCreateDto newDeviceType)
    {
        var createdDeviceType = await _deviceTypeRepository.CreateDeviceTypeAsync(_mappings.DeviceTypeCreateDtoToDeviceType(newDeviceType));

        return CreatedAtAction(nameof(CreateNewDeviceTypeAsync), _mappings.DeviceTypeToDeviceTypePresentDto(createdDeviceType!));
    }

    /// <summary>
    /// Gets info of speciffic device type.
    /// </summary>
    [Authorize]
    [HttpGet("{deviceTypeId:int}")]
    [ProducesResponseType(typeof(DeviceTypePresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDeviceTypeByIdAsync([FromRoute] int deviceTypeId)
    {
        var deviceType = await _deviceTypeRepository.GetDeviceTypeByIdAsync(deviceTypeId);

        return  Ok(_mappings.DeviceTypeToDeviceTypePresentDto(deviceType));
    }

    /// <summary>
    /// Gets info of all device types.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "Production ")]
    [ProducesResponseType(typeof(IEnumerable<DeviceTypePresentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllDeviceTypesAsync()
    {
        var deviceTypes = await _deviceTypeRepository.GetAllDeviceTypesAsync();

        return Ok(_mappings.DeviceTypesToDeviceTypePresentDtos(deviceTypes));
    }

    ///<summary>
    /// Updates name of device type info by id.
    /// </summary>
    [HttpPut("{deviceTypeId:int}")]
    [Authorize(Policy = "Production ")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDeviceTypeInfoAsync([FromRoute]int deviceTypeId, [FromForm]DeviceTypeCreateDto deviceUpdateDto)
    {
        await _deviceTypeRepository.UpdateDeviceTypeAsync(deviceTypeId, _mappings.DeviceTypeCreateDtoToDeviceType(deviceUpdateDto));

        return NoContent();
    }

    ///<summary>
    /// Deletes device type selected by id.
    /// </summary>
    [HttpDelete("{deviceTypeId:int}")]
    [Authorize(Policy = "Production ")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeviceTypeByIdAsync([FromRoute] int deviceTypeId)
    {
        await _deviceTypeRepository.DeleteDeviceTypeAsync(deviceTypeId);

        return NoContent();
    }
}
