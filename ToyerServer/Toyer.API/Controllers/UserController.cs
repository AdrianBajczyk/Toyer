using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Toyer.Logic.Dtos.User;
using Toyer.Logic.Exceptions.FailResponses.Derived.User;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Authorization.AuthorizationHandlers;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class UserController(IUserRepository userRepository, 
    IUserMapings mappings,  
    IAuthorizationService authorizationService) 
    : ControllerBase
{
    private readonly IUserMapings _mappings = mappings;
    private readonly IAuthorizationService _authorizationService = authorizationService;
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Creates account for new user and sends confirmation email to selected address.
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateNewUserAsync([FromBody] UserCreateDto newUserDto)
    {


        var result = await _userRepository.RegisterNewUserAsync(_mappings.UserCreateDtoToUser(newUserDto), newUserDto.Password);

        return CreatedAtAction(nameof(CreateNewUserAsync), new CustomResponse { message = $"User: {newUserDto.UserName} created.", status = 201 });
    }

    /// <summary>
    /// Logs user in.
    /// </summary>
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] UserLogin request)
    {
        var (refreshToken, response) = await _userRepository.LoginAsync(request.Email, request.Password);

        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
        });

        return Ok(response);
    }

    /// <summary>
    /// Sends email confirmation link to users email.
    /// </summary>
    [HttpPost("Email")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    
    public async Task<IActionResult> ResendEmailConfirmationLink([FromForm] UserEmail userInput)
    {
        await _userRepository.ResendEmailConfirmationLink(userInput.Email);

        return NoContent();
    }


    /// <summary>
    /// Confirms user email by token.
    /// </summary>
    [HttpGet("Email")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        await _userRepository.ConfirmEmailAsync(token, email);

        return NoContent();
    }

    ///// <summary>
    ///// Confirms user password by token.
    ///// </summary>
    //[HttpGet("Password")]
    //[AllowAnonymous]
    //[ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status204NoContent)]
    //[ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    //[ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> SendPasswordResetLink(UserEmail input)
    //{
    //    await _userRepository.SendPasswordResetLink(input.Email);

    //    return NoContent();
    //}


    /// <summary>
    /// Gets login of specific user.
    /// </summary>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(UserPresentShortDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetShortByIdAsync([FromRoute] string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        return Ok(_mappings.UserToUserPresentShortDto(user));
    }

    /// <summary>
    /// Gets all users.
    /// </summary>
    [HttpGet]
    //[Authorize(Policy = "Production")]
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
    public async Task<IActionResult> GetExtendedByIdAsync([FromRoute] string userId)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, PermissionRequirements.EditPermission);
        if (!authorizationResult.Succeeded) throw new AccessException();

        var user = await _userRepository.GetUserByIdAsync(userId);

        return Ok(_mappings.UserToUserPresentLongDto(user));
    }

    

    /// <summary>
    /// Updates account address by applying changes from non-null properties.
    /// </summary>
    [HttpPut("Address/{userId}")]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAddressByIdAsync([FromRoute] string userId, [FromForm] AddressDto addressUpdatesDtoFromUser)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, PermissionRequirements.EditPermission);
        if (!authorizationResult.Succeeded) throw new AccessException();

        await _userRepository.UpdateAddressAsync(userId, _mappings.AddressDtoToAddress(addressUpdatesDtoFromUser));
        return NoContent();
    }

    /// <summary>
    /// Updates account personal information by applying changes from non-null properties.
    /// </summary>
    [HttpPut("PersonalInfo/{userId}")]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePersonalInfoByIdAsync([FromRoute] string userId, [FromForm] PersonalInfoDto personalInfoUpdates)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, PermissionRequirements.EditPermission);
        if (!authorizationResult.Succeeded) throw new AccessException();

        await _userRepository.UpdatePersonalInfoPatchAsync(userId, _mappings.PersonalInfoDtoToPersonalInfo(personalInfoUpdates));
        return NoContent();
    }

    /// <summary>
    /// Updates account contcact info by applying changes from non-null properties.
    /// </summary>
    [HttpPut("Conctact/{userId}")]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateContactInfoByIdAsync([FromRoute] string userId, [FromForm] ContactDto contactUpdates)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, PermissionRequirements.EditPermission);
        if (!authorizationResult.Succeeded) throw new AccessException();

        await _userRepository.UpdateContactInfoAsync(userId, contactUpdates.Email, contactUpdates.PhoneNumber);
        return NoContent();
    }

    /// <summary>
    /// Deletes account of existing user
    /// </summary>
    [HttpDelete("{userId}")]
    [ProducesResponseType(typeof(PersonalInfoDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteUserByIdAsync([FromRoute] string userId)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userId, PermissionRequirements.DeletePermission);
        if (!authorizationResult.Succeeded) throw new AccessException();

        await _userRepository.DeleteUserAsync(userId);
        return NoContent();
    }



}