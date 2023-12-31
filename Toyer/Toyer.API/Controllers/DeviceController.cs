using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDeviceRepository _deviceRepository;

    public DeviceController(IMapper mapper, IDeviceRepository deviceRepository)
    {
        _mapper = mapper;
        _deviceRepository = deviceRepository;
    }

    ///<summary>
    /// Create a new device and enroll it in Azure IoT hub. 
    /// </summary>

}
