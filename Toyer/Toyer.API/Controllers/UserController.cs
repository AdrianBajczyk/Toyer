using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.User;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class UserController : ControllerBase
{
    private readonly IUserMapings _mappings;
    private readonly IDeviceAssignRepository _deviceAssignRepository;
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository, IUserMapings mappings, IDeviceAssignRepository deviceAssignRepository)
    {
        _userRepository = userRepository;
        _mappings = mappings;
        _deviceAssignRepository = deviceAssignRepository;
    }


    /// <summary>
    /// Logs user in.
    /// </summary>
    [HttpPost("Login")]
    public async Task<IActionResult> Authenticate([FromForm] UserLogin request)
    {
        var result = await _userRepository.LoginAsync(request.Email, request.Password);

        return new ObjectResult (result);
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

        return user is null
            ? NotFound(new CustomResponse { Message = "User not found", StatusCode = "404" })
            : Ok(_mappings.UserToUserPresentShortDto(user));
    }
    /// <summary>
    /// Gets all users.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserPresentShortDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUsersAsync()
    {
        var allUsers = await _userRepository.GetUsersAsync();

        return !allUsers.Any()
            ? Accepted(new CustomResponse() { Message = "No users in database yet.", StatusCode = "202" })
            : Ok(_mappings.UsersToUserPresentShortDtos(allUsers));
    }

    /// <summary>
    /// Gets account info for authorized user.
    /// </summary>
    [HttpGet("extended/{userId}")]
    [ProducesResponseType(typeof(UserPresentLongDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExtendedById([FromRoute] string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        return user is null
            ? NotFound(new CustomResponse { Message = "User not found", StatusCode = "404" })
            : Ok(_mappings.UserToUserPresentLongDto(user));
    }

    /// <summary>
    /// Creates account for new user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateNewUser([FromForm] UserCreateDto newUserDto)
    {
        var result = await _userRepository.RegisterNewUserAsync(_mappings.UserCreateDtoToUser(newUserDto), newUserDto.Password);

        return result.Succeeded
            ? CreatedAtAction(nameof(CreateNewUser), new CustomResponse { Message = $"User: {newUserDto.UserName} created.", StatusCode = "201" })
            : BadRequest(new CustomResponse { Message = $"Error: {string.Join(", ", result.Errors.Select(e => e.Description))}", StatusCode = "400" });
    }

    /// <summary>
    /// Updates account address by applying changes from non-null properties.
    /// </summary>
    [HttpPut("Address/{userId}")]
    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAddressById([FromRoute] string userId, [FromForm] AddressDto addressUpdatesDtoFromUser)
    {
        var updatedAddress = await _userRepository.UpdateAddressAsync(userId, _mappings.AddressDtoToAddress(addressUpdatesDtoFromUser));

        return updatedAddress is null
            ? NotFound(new CustomResponse { Message = "User not found", StatusCode = "404" })
            : Ok(_mappings.AddressToAddressDto(updatedAddress));
    }

    /// <summary>
    /// Updates account personal information by applying changes from non-null properties.
    /// </summary>
    [HttpPut("PersonalInfo/{userId}")]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePersonalInfoById([FromRoute] string userId, [FromForm] PersonalInfoDto personalInfoUpdates)
    {
        var updatedPersonalInfo = await _userRepository.UpdatePersonalInfoPatchAsync(userId, _mappings.PersonalInfoDtoToPersonalInfo(personalInfoUpdates));

        return updatedPersonalInfo is null
            ? NotFound(new CustomResponse { Message = "User not found.", StatusCode = "404" })
            : Ok(_mappings.PersonalInfoToPersonalInfoDto(updatedPersonalInfo));
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
        var result = await _userRepository.UpdateContactInfoAsync(userId, contactUpdates.Email, contactUpdates.PhoneNumber);

        if (result == null) return NotFound(new CustomResponse { Message = "User not found.", StatusCode = "404" });

        return result.Succeeded
            ? Ok(new CustomResponse { Message = $"Updated.", StatusCode = "200" })
            : new ObjectResult(new CustomResponse { Message = $"Error: {string.Join(", ", result.Errors.Select(e => e.Description))}", StatusCode = result.Errors.First().Code });
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
        var result = await _userRepository.DeleteUserAsync(userId);
        if (result!.Succeeded) await _deviceAssignRepository.DeleteUserAsync(userId);
            
        return result.Succeeded 
            ? Ok(new CustomResponse { Message = $"Deleted.", StatusCode = "200" })
            : new ObjectResult(new CustomResponse { Message = $"Error: {string.Join(", ", result.Errors.Select(e => e.Description))}", StatusCode = result.Errors.First().Code });

    }



}