using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.DeviceType;
using Toyer.Logic.Dtos.User;
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
    /// Creates account for new user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DeviceTypePresentDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewDeviceType([FromForm] DeviceTypeCreateDto newDeviceType)
    {
        var createDeviceType = await _deviceTypeRepository.CreateDeviceTypeAsync(_mappings.DeviceTypeCreateDtoToDeviceType(newDeviceType));

        return CreatedAtAction(nameof(CreateNewDeviceType), _mappings.DeviceTypeToDeviceTypePresentDto(createDeviceType));
    }
}
