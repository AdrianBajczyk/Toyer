using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeviceTypeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDeviceTypeRepository _deviceTypeRepository;

    public DeviceTypeController(IDeviceTypeRepository deviceTypeRepository, IMapper mapper)
    {
        _deviceTypeRepository = deviceTypeRepository;
        _mapper = mapper;
    }
}
