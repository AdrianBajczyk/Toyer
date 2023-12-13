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
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
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

        UserPresentLongDto userLongDto = MapUserDataToLongDto(user);

        return Ok(userLongDto);
    }

    [HttpPost]
    [Route("/createNew")]
    public async Task<IActionResult> CreateNewUser([FromForm] UserCreateDto newUser)
    {
        var userToDb = _mapper.Map<User>(newUser);
        var personalInfoToDb = _mapper.Map<PersonalInfo>(newUser);
        var addressToDb = _mapper.Map<Address>(newUser);

        var createdUser = await _userRepository.CreateNewUserAsync(userToDb, personalInfoToDb, addressToDb);

        UserPresentLongDto userLongDto = MapUserDataToLongDto(createdUser);

        return CreatedAtAction(nameof(CreateNewUser), userLongDto);
    }


    [HttpPut]
    [Route("/updateAddress/{userId:Guid}")]
    public async Task<IActionResult> UpdateAddressById([FromRoute] Guid userId, [FromForm] UserAddressDto addressUpdatesFromUserDto)
    {
        var addressUpdatesFromUser = _mapper.Map<Address>(addressUpdatesFromUserDto);

        var updatedAddress = await _userRepository.UpdateAddressPatchAsync(userId, addressUpdatesFromUser);

        if (updatedAddress == null) return NotFound("User not found.");

        var updatedAddressDto = _mapper.Map<UserAddressDto>(updatedAddress);

        return Ok(updatedAddressDto);
    }

    []


    private UserPresentLongDto MapUserDataToLongDto(User? createdUser)
    {
        var userLongDto = _mapper.Map<UserPresentLongDto>(createdUser);
        userLongDto.UserPersonalInfo = _mapper.Map<UserPersonalInfoDto>(createdUser.PersonalInfo);
        userLongDto.UserAddress = _mapper.Map<UserAddressDto>(createdUser.PersonalInfo.Address);
        userLongDto.UserPresentShort = _mapper.Map<UserPresentShortDto>(createdUser);
        return userLongDto;
    }

}

