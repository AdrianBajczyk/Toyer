using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Exceptions.FailResponses.Derived.User;
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
    public async Task<User> GetUserByIdAsync(string userId)
    {
        return await _dbContext.Users
                    .Include(u => u.PersonalInfo)
                    .ThenInclude(p => p.Address)
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId)
                    ?? throw new UserNotFoundException(userId);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var users = await _dbContext.Users
                .Include(u => u.PersonalInfo)
                .ThenInclude(p => p.Address)
                .ToListAsync();

        return users.IsNullOrEmpty()
            ? throw new UsersTableEmptyException()
            : users;
    }

    public async Task<User> RegisterNewUserAsync(User newUser, string password)
    {
        var result = await _userManager.CreateAsync(newUser);

        if (!result.Succeeded) throw new Exception();

        await _userManager.AddToRoleAsync(newUser, "RegisteredUser");
        await _userManager.AddPasswordAsync(newUser, password);

        return newUser;
    }
    public async Task UpdateAddressAsync(string userId, Address updatesFromUser)
    {
        var userToUpdate = await GetUserByIdAsync(userId) 
            ?? throw new UserNotFoundException(userId);

        var addressToUpdate = userToUpdate.PersonalInfo!.Address!;

        if (updatesFromUser.State != null) addressToUpdate.State = updatesFromUser.State;
        if (updatesFromUser.Street != null) addressToUpdate.Street = updatesFromUser.Street;
        if (updatesFromUser.Street != null) addressToUpdate.Street = updatesFromUser.Street;
        if (updatesFromUser.Street != null) addressToUpdate.Street = updatesFromUser.Street;
        if (updatesFromUser.City != null) addressToUpdate.City = updatesFromUser.City;
        if (updatesFromUser.Country != null) addressToUpdate.Country = updatesFromUser.Country;
        if (updatesFromUser.PostalCode != null) addressToUpdate.PostalCode = updatesFromUser.PostalCode;

        await _dbContext.SaveChangesAsync();

    }
    public async Task UpdatePersonalInfoPatchAsync(string userId, PersonalInfo updatesFromUser)
    {
        var userToUpdate = await GetUserByIdAsync(userId) 
            ?? throw new UserNotFoundException(userId);

        var personalInfoToUpdate = userToUpdate.PersonalInfo!;

        if (updatesFromUser.Name != null) personalInfoToUpdate.Name = updatesFromUser.Name;
        if (updatesFromUser.Surname != null) personalInfoToUpdate.Surname = updatesFromUser.Surname;
        if (updatesFromUser.BirthDate != default) personalInfoToUpdate.BirthDate = updatesFromUser.BirthDate;

        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteUserAsync(string userId)
    {
        var userToDelete = await GetUserByIdAsync(userId) 
            ?? throw new UserNotFoundException(userId);

        await _userManager.DeleteAsync(userToDelete);

    }

    public async Task UpdateContactInfoAsync(string userId, string? email, string? phoneNumber)
    {
        var userToUpdate = await GetUserByIdAsync(userId) 
            ?? throw new UserNotFoundException(userId);

        if (!string.IsNullOrWhiteSpace(email)) await _userManager.SetEmailAsync(userToUpdate, email);

        if (!string.IsNullOrWhiteSpace(phoneNumber)) await _userManager.SetPhoneNumberAsync(userToUpdate, phoneNumber);

         await _userManager.UpdateAsync(userToUpdate);
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
