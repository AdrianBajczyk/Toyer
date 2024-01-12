using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Authorization;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlUserRepository : IUserRepository

{
    private readonly UsersDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public SqlUserRepository(UsersDbContext usersDbContext, UserManager<User> userManager, ITokenService tokenService)
    {
        _dbContext = usersDbContext;
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<User?> GetUserByIdAsync(string id)
    => await _dbContext.Users
            .Include(u => u.PersonalInfo)
            .ThenInclude(p => p.Address)
            .FirstOrDefaultAsync(u => u.Id.ToString() == id);
    

    public async Task<List<User>?> GetUsersAsync()
    => await _dbContext.Users
            .Include(u => u.PersonalInfo)
            .ThenInclude(p => p.Address)
            .ToListAsync();
    
    public async Task<IdentityResult> RegisterNewUserAsync(User newUser, string password)
    {
        var result = await _userManager.CreateAsync(newUser);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, "RegisteredUser");
            result = await _userManager.AddPasswordAsync(newUser, password);
        }

        return result;
    }
    public async Task<Address?> UpdateAddressAsync(string userId, Address updatesFromUser)
    {
        var userToUpdate = await GetUserByIdAsync(userId);

        if (userToUpdate == null) return null;

        var addressToUpdate = userToUpdate.PersonalInfo!.Address!;

        if (updatesFromUser.State != null) addressToUpdate.State = updatesFromUser.State;
        if (updatesFromUser.Street != null) addressToUpdate.Street = updatesFromUser.Street;
        if (updatesFromUser.Street != null) addressToUpdate.Street = updatesFromUser.Street;
        if (updatesFromUser.Street != null) addressToUpdate.Street = updatesFromUser.Street;
        if (updatesFromUser.City != null) addressToUpdate.City = updatesFromUser.City;
        if (updatesFromUser.Country != null) addressToUpdate.Country = updatesFromUser.Country;
        if (updatesFromUser.PostalCode != null) addressToUpdate.PostalCode = updatesFromUser.PostalCode;

        await _dbContext.SaveChangesAsync();

        return addressToUpdate;
    }
    public async Task<PersonalInfo?> UpdatePersonalInfoPatchAsync(string userId, PersonalInfo updatesFromUser)
    {
        var userToUpdate = await GetUserByIdAsync(userId);

        if (userToUpdate == null) return null;

        var personalInfoToUpdate = userToUpdate.PersonalInfo!;

        if (updatesFromUser.Name != null) personalInfoToUpdate.Name = updatesFromUser.Name;
        if (updatesFromUser.Surname != null) personalInfoToUpdate.Surname = updatesFromUser.Surname;
        if (updatesFromUser.BirthDate != default) personalInfoToUpdate.BirthDate = updatesFromUser.BirthDate;

        await _dbContext.SaveChangesAsync();

        return personalInfoToUpdate;
    }
    public async Task<IdentityResult?> DeleteUserAsync(string Id)
    {
        var userToDelete = await GetUserByIdAsync(Id);

        if (userToDelete == null) return IdentityResult.Failed(new IdentityError { Description = "User not found.", Code = "404" });

        var result = await _userManager.DeleteAsync(userToDelete);

        return result;
    }

    public async Task<IdentityResult> UpdateContactInfoAsync(string userId, string? email, string? phoneNumber)
    {
        var userToUpdate = await GetUserByIdAsync(userId);

        if (userToUpdate == null) return IdentityResult.Failed(new IdentityError { Description = "User not found.", Code = "404" });

        if (!string.IsNullOrWhiteSpace(email))
        {
            var setEmailResult = await _userManager.SetEmailAsync(userToUpdate, email);

            if (!setEmailResult.Succeeded) return setEmailResult;
        }

        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            var setPhoneNumberResult = await _userManager.SetPhoneNumberAsync(userToUpdate, phoneNumber);

            if (!setPhoneNumberResult.Succeeded) return setPhoneNumberResult;
        }

        return await _userManager.UpdateAsync(userToUpdate);
    }

    public async Task<AuthorizationResponse> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!isPasswordValid || user == null) return new AuthorizationResponse { Message = "Invalid username or password." , StatusCode = "401"};

        var accessToken = _tokenService.CreateToken(user);

        return new AuthorizationResponse { Message = "Login succeed.", StatusCode = "200", Token =  accessToken };
    }
}
