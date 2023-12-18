using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Toyer.API.Exceptions;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;
using Toyer.Logic.Exceptions;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class UserController : ControllerBase
{
    private readonly IUserControllerMapings _mappings;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository, IUserControllerMapings mappings, IMapper mapper)
    {
        _userRepository = userRepository;
        _mappings = mappings;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets login of specific user.
    /// </summary>
    [HttpGet]
    [Route("{userId:Guid}")]
    [ProducesResponseType(typeof(UserPresentShortDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetShortById([FromRoute] Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null) return NotFound(new ErrorDetails { Message = "User not found" });

        var userShortDto = _mapper.Map<UserPresentShortDto>(user);

        return Ok(userShortDto);
    }

    /// <summary>
    /// Gets account info for authorized user.
    /// </summary>
    [HttpGet]
    [Route("extended/{userId:Guid}")]
    [ProducesResponseType(typeof(UserPresentLongDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExtendedById([FromRoute] Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null) return NotFound(new ErrorDetails { Message = "User not found" });

        var userLongDto = _mappings.MapUserDataToLongDto(user);

        return Ok(userLongDto);
    }

    /// <summary>
    /// Create account for new user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(UserPresentLongDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewUser([FromForm] UserCreateDto newUserDto)
    {
        var userToDb = _mappings.MapUserCreateDtoToUser(newUserDto);

        var createdUser = await _userRepository.CreateNewUserAsync(userToDb);

        var userLongDto = _mappings.MapUserDataToLongDto(createdUser);

        return CreatedAtAction(nameof(CreateNewUser), userLongDto);
    }

    /// <summary>
    /// Updates account address by applying changes from non-null properties.
    /// </summary>
    [HttpPut]
    [Route("Address/{userId:Guid}")]
    [ProducesResponseType(typeof(UserAddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateAddressById([FromRoute] Guid userId, [FromForm] UserAddressDto addressUpdatesDtoFromUser)
    {
        var addressUpdatesFromUser = _mapper.Map<Address>(addressUpdatesDtoFromUser);

        var updatedAddress = await _userRepository.UpdateAddressPatchAsync(userId, addressUpdatesFromUser);

        if (updatedAddress == null) return NotFound(new ErrorDetails { Message = "User not found"});

        var updatedAddressDto = _mapper.Map<UserAddressDto>(updatedAddress);

        return Ok(updatedAddressDto);
    }

    /// <summary>
    /// Updates account personal information by applying changes from non-null properties.
    /// </summary>
    [HttpPut]
    [Route("PersonalInfo/{userId:guid}")]
    [ProducesResponseType(typeof(UserPersonalInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePersonalInfoById([FromRoute] Guid userId, [FromForm] UserPersonalInfoDto personalInfoDtoFromUser)
    {
        var personalInfoUpdatesFromUser = _mapper.Map<PersonalInfo>(personalInfoDtoFromUser);

        var updatedPersonalInfo = await _userRepository.UpdatePersonalInfoPatchAsync(userId, personalInfoUpdatesFromUser);

        if (updatedPersonalInfo == null) return NotFound(new ErrorDetails { Message = "User not found" });

        var updatedPersonalInfoDto = _mapper.Map<UserPersonalInfoDto>(updatedPersonalInfo);

        return Ok(updatedPersonalInfoDto);
    }


    

}

