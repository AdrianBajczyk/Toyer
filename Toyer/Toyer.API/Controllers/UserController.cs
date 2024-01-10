//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Toyer.Logic.Dtos.User;
//using Toyer.Logic.Responses;
//using Toyer.Logic.Services.Repositories.Interfaces;

//namespace Toyer.API.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//[Produces("application/json")]
//[ProducesResponseType(StatusCodes.Status500InternalServerError)]
//public class UserController : ControllerBase
//{
//    private readonly IUserMapings _mappings;
//    private readonly IUserRepository _userRepository;

//    public UserController(IUserRepository userRepository, IUserMapings mappings)
//    {
//        _userRepository = userRepository;
//        _mappings = mappings;
//    }

//    /// <summary>
//    /// Gets login of specific user.
//    /// </summary>
//    [HttpGet("{userId:Guid}")]
//    [ProducesResponseType(typeof(UserPresentShortDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> GetShortById([FromRoute] Guid userId)
//    {
//        var user = await _userRepository.GetUserByIdAsync(userId);

//        return user is null
//            ? NotFound(new CustomResponse { Message = "User not found", StatusCode = 404 })
//            : Ok(_mappings.UserToUserPresentShortDto(user));
//    }
//    /// <summary>
//    /// Gets all users.
//    /// </summary>
//    [HttpGet, Authorize]
//    [ProducesResponseType(typeof(IEnumerable<UserPresentShortDto>), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status202Accepted)]
//    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> GetUsersAsync()
//    {
//        var allUsers = await _userRepository.GetUsersAsync();

//        return !allUsers.Any()
//            ? Accepted(new CustomResponse() { Message = "No users in database yet.", StatusCode = 202 })
//            : Ok(_mappings.UsersToUserPresentShortDtos(allUsers));
//    }

//    /// <summary>
//    /// Gets account info for authorized user.
//    /// </summary>
//    [HttpGet("extended/{userId:Guid}")]
//    [ProducesResponseType(typeof(UserPresentLongDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> GetExtendedById([FromRoute] Guid userId)
//    {
//        var user = await _userRepository.GetUserByIdAsync(userId);

//        return user is null
//            ? NotFound(new CustomResponse { Message = "User not found", StatusCode = 404 })
//            : Ok(_mappings.UserToUserPresentLongDto(user));
//    }

//    /// <summary>
//    /// Creates account for new user
//    /// </summary>
//    [HttpPost]
//    [ProducesResponseType(typeof(UserPresentLongDto), StatusCodes.Status201Created)]
//    public async Task<IActionResult> CreateNewUser([FromForm] UserCreateDto newUserDto)
//    {
//        var createdUser = await _userRepository.CreateNewUserAsync(_mappings.UserCreateDtoToUser(newUserDto));

//        return CreatedAtAction(nameof(CreateNewUser), _mappings.UserToUserPresentLongDto(createdUser));
//    }

//    /// <summary>
//    /// Updates account address by applying changes from non-null properties.
//    /// </summary>
//    [HttpPut("Address/{userId:Guid}")]
//    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> UpdateAddressById([FromRoute] Guid userId, [FromForm] AddressDto addressUpdatesDtoFromUser)
//    {
//        var updatedAddress = await _userRepository.UpdateAddressPatchAsync(userId, _mappings.AddressDtoToAddress(addressUpdatesDtoFromUser));

//        return updatedAddress is null
//            ? NotFound(new CustomResponse { Message = "User not found", StatusCode = 404 })
//            : Ok(_mappings.AddressToAddressDto(updatedAddress));
//    }

//    /// <summary>
//    /// Updates account personal information by applying changes from non-null properties.
//    /// </summary>
//    [HttpPut("PersonalInfo/{userId:guid}")]
//    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> UpdatePersonalInfoById([FromRoute] Guid userId, [FromForm] PersonalInfoDto personalInfoDtoFromUser)
//    {
//        var updatedPersonalInfo = await _userRepository.UpdatePersonalInfoPatchAsync(userId, _mappings.PersonalInfoDtoToPersonalInfo(personalInfoDtoFromUser));

//        return updatedPersonalInfo is null
//            ? NotFound(new CustomResponse { Message = "User not found", StatusCode = 404 })
//            : Ok(_mappings.PersonalInfoToPersonalInfoDto(updatedPersonalInfo));
//    }

//    /// <summary>
//    /// Deletes account of existing user
//    /// </summary>
//    [HttpDelete("{userId:Guid}")]
//    [ProducesResponseType(typeof(UserPresentShortDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
//    public async Task<ActionResult> DeleteUserById([FromRoute] Guid userId)
//    {
//        var deletedUser = await _userRepository.DeleteUserAsync(userId);

//        return deletedUser is null
//            ? NotFound(new CustomResponse { Message = "User not found.", StatusCode = 404 })
//            : Ok(_mappings.UserToUserPresentShortDto(deletedUser));
//    }

//    ///<summary>
//    /// Assigns device to user.
//    /// </summary>
//    [HttpPut("{userId:Guid}")]
//    [ProducesResponseType(typeof(UserPresentLongDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
//    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
//    public async Task<IActionResult> AssignDeviceToUserAsync([FromRoute] Guid userId, [FromForm] Guid deviceId)
//    {
//        var result = await _userRepository.AssignDeviceToUserAsync(userId, deviceId);

//        return result.StatusCode == 200
//            ? Ok(result)
//            : new ObjectResult(result);
//    }

//}