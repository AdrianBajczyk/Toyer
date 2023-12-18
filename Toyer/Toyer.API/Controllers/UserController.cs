using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet]
    [Route("/short/{userId:Guid}")]
    public async Task<IActionResult> GetShortById([FromRoute] Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null) return NotFound("User not found.");

        var userShortDto = _mapper.Map<UserPresentShortDto>(user);

        return Ok(userShortDto);
    }

    [HttpGet]
    [Route("/extended/{userId:Guid}")]
    public async Task<IActionResult> GetExtendedById([FromRoute] Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null) return NotFound("User not found.");

        var userLongDto = _mappings.MapUserDataToLongDto(user);

        return Ok(userLongDto);
    }

    [HttpPost]
    [Route("/createNew")]
    public async Task<IActionResult> CreateNewUser([FromForm] UserCreateDto newUserDto)
    {
        var userToDb = _mappings.MapUserCreateDtoToUser(newUserDto);

        var createdUser = await _userRepository.CreateNewUserAsync(userToDb);

        var userLongDto = _mappings.MapUserDataToLongDto(createdUser);

        return CreatedAtAction(nameof(CreateNewUser), userLongDto);
    }


    [HttpPut]
    [Route("/updateAddress/{userId:Guid}")]
    public async Task<IActionResult> UpdateAddressById([FromRoute] Guid userId, [FromForm] UserAddressDto addressUpdatesDtoFromUser)
    {
        var addressUpdatesFromUser = _mapper.Map<Address>(addressUpdatesDtoFromUser);

        var updatedAddress = await _userRepository.UpdateAddressPatchAsync(userId, addressUpdatesFromUser);

        if (updatedAddress == null) return NotFound("User not found.");

        var updatedAddressDto = _mapper.Map<UserAddressDto>(updatedAddress);

        return Ok(updatedAddressDto);
    }

    [HttpPut]
    [Route("/updatePersonalInfo/{userId:guid}")]
    public async Task<IActionResult> UpdatePersonalInfoById([FromRoute] Guid userId, [FromForm] UserPersonalInfoDto personalInfoDtoFromUser)
    {
        var personalInfoUpdatesFromUser = _mapper.Map<PersonalInfo>(personalInfoDtoFromUser);

        var updatedPersonalInfo = await _userRepository.UpdatePersonalInfoPatchAsync(userId, personalInfoUpdatesFromUser);

        if (updatedPersonalInfo == null) return NotFound("User not found.");

        var updatedPersonalInfoDto = _mapper.Map<UserPersonalInfoDto>(updatedPersonalInfo);

        return Ok(updatedPersonalInfoDto);
    }


    

}

