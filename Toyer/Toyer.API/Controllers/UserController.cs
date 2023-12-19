﻿using Microsoft.AspNetCore.Mvc;
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
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository, IUserControllerMapings mappings)
    {
        _userRepository = userRepository;
        _mappings = mappings;
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

        return user is null
            ? NotFound(new ErrorDetails { Message = "User not found" })
            : Ok(_mappings.UserToUserPresentShortDto(user));
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

        return user is null 
            ? NotFound(new ErrorDetails { Message = "User not found" })
            : Ok(_mappings.UserToUserPresentLongDto(user));
    }

    /// <summary>
    /// Creates account for new user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(UserPresentLongDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewUser([FromForm] UserCreateDto newUserDto)
    {
        var createdUser = await _userRepository.CreateNewUserAsync(_mappings.UserCreateDtoToUser(newUserDto));

        return CreatedAtAction(nameof(CreateNewUser), _mappings.UserToUserPresentLongDto(createdUser));
    }

    /// <summary>
    /// Updates account address by applying changes from non-null properties.
    /// </summary>
    [HttpPut]
    [Route("Address/{userId:Guid}")]
    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAddressById([FromRoute] Guid userId, [FromForm] AddressDto addressUpdatesDtoFromUser)
    {
        var updatedAddress = await _userRepository.UpdateAddressPatchAsync(userId, _mappings.AddressDtoToAddress(addressUpdatesDtoFromUser));

        return updatedAddress is null
            ? NotFound(new ErrorDetails { Message = "User not found" })
            : Ok(_mappings.AddressToAddressDto(updatedAddress));
    }

    /// <summary>
    /// Updates account personal information by applying changes from non-null properties.
    /// </summary>
    [HttpPut]
    [Route("PersonalInfo/{userId:guid}")]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePersonalInfoById([FromRoute] Guid userId, [FromForm] PersonalInfoDto personalInfoDtoFromUser)
    {
        var updatedPersonalInfo = await _userRepository.UpdatePersonalInfoPatchAsync(userId, _mappings.PersonalInfoDtoToPersonalInfo(personalInfoDtoFromUser));

        return updatedPersonalInfo is null
            ? NotFound(new ErrorDetails { Message = "User not found" })
            : Ok(_mappings.PersonalInfoToPersonalInfoDto(updatedPersonalInfo));
    }

}

