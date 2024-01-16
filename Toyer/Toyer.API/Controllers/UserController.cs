﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;
using Toyer.Logic.Exceptions.FailResponses.Derived.User;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class UserController(IUserRepository userRepository, 
    IUserMapings mappings, 
    IDeviceAssignRepository deviceAssignRepository, 
    IAuthorizationService authorizationService) 
    : ControllerBase
{
    private readonly IUserMapings _mappings = mappings;
    private readonly IDeviceAssignRepository _deviceAssignRepository = deviceAssignRepository;
    private readonly IAuthorizationService _authorizationService = authorizationService;
    private readonly IUserRepository _userRepository = userRepository;


    /// <summary>
    /// Logs user in.
    /// </summary>
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromForm] UserLogin request)
    {
        var result = await _userRepository.LoginAsync(request.Email, request.Password);

        return new ObjectResult(result);
    }


    /// <summary>
    /// Gets login of specific user.
    /// </summary>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(UserPresentShortDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetShortById([FromRoute] string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        return Ok(_mappings.UserToUserPresentShortDto(user));
    }
    /// <summary>
    /// Gets all users.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "ProductionTasks")]
    [ProducesResponseType(typeof(IEnumerable<UserPresentShortDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUsersAsync()
    {
        var allUsers = await _userRepository.GetUsersAsync();

        return Ok(_mappings.UsersToUserPresentShortDtos(allUsers));
    }

    /// <summary>
    /// Gets account info for authorized user.
    /// </summary>
    [HttpGet("extended/{userId}")]
    [ProducesResponseType(typeof(UserPresentLongDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExtendedById([FromRoute] string userId)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, "OwnerOrPrivilegedPolicy");
        if (!authorizationResult.Succeeded) throw new AccessException();

        var user = await _userRepository.GetUserByIdAsync(userId);

        return Ok(_mappings.UserToUserPresentLongDto(user));
    }

    /// <summary>
    /// Creates account for new user
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateNewUser([FromForm] UserCreateDto newUserDto)
    {
        var result = await _userRepository.RegisterNewUserAsync(_mappings.UserCreateDtoToUser(newUserDto), newUserDto.Password);

        return CreatedAtAction(nameof(CreateNewUser), new CustomResponse { Message = $"User: {newUserDto.UserName} created.", StatusCode = 201 });
    }

    /// <summary>
    /// Updates account address by applying changes from non-null properties.
    /// </summary>
    [HttpPut("Address/{userId}")]
    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAddressById([FromRoute] string userId, [FromForm] AddressDto addressUpdatesDtoFromUser)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, "OwnerOrPrivilegedPolicy");
        if (!authorizationResult.Succeeded) throw new AccessException();

        await _userRepository.UpdateAddressAsync(userId, _mappings.AddressDtoToAddress(addressUpdatesDtoFromUser));
        return NoContent();
    }

    /// <summary>
    /// Updates account personal information by applying changes from non-null properties.
    /// </summary>
    [HttpPut("PersonalInfo/{userId}")]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePersonalInfoById([FromRoute] string userId, [FromForm] PersonalInfoDto personalInfoUpdates)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, "OwnerOrPrivilegedPolicy");
        if (!authorizationResult.Succeeded) throw new AccessException();

        await _userRepository.UpdatePersonalInfoPatchAsync(userId, _mappings.PersonalInfoDtoToPersonalInfo(personalInfoUpdates));
        return NoContent();
    }

    /// <summary>
    /// Updates account contcact info by applying changes from non-null properties.
    /// </summary>
    [HttpPut("Conctact/{userId}")]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateContactInfoById([FromRoute] string userId, [FromForm] ContactDto contactUpdates)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, "OwnerOrPrivilegedPolicy");
        if (!authorizationResult.Succeeded) throw new AccessException();

        await _userRepository.UpdateContactInfoAsync(userId, contactUpdates.Email, contactUpdates.PhoneNumber);
        return NoContent();
    }

    /// <summary>
    /// Deletes account of existing user
    /// </summary>
    [HttpDelete("{userId}")]
    [ProducesResponseType(typeof(UserPresentShortDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteUserById([FromRoute] string userId)
    {

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, "OwnerOrPrivilegedPolicy");
        if (!authorizationResult.Succeeded) throw new AccessException();

        await _userRepository.DeleteUserAsync(userId);
        return NoContent();
    }



}