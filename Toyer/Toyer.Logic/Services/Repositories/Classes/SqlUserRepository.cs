using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Exceptions.FailResponses.Derived.User;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Authorization.Token;
using Toyer.Logic.Services.EmailService;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.Logic.Services.Repositories.Classes;

public class SqlUserRepository : IUserRepository

{
    private readonly UsersDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IEmailSender _emailSender;

    public SqlUserRepository(UsersDbContext usersDbContext, UserManager<User> userManager, ITokenService tokenService, IEmailSender emailSender)
    {
        _dbContext = usersDbContext;
        _userManager = userManager;
        _tokenService = tokenService;
        _emailSender = emailSender;
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
        await _userManager.CreateAsync(newUser);

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

    public async Task<AuthenticationResponse> LoginAsync(string email, string password)
    {
        var user = await _userManager.Users.Include(u => u.RefreshTokenModel).FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return new AuthenticationResponse { Message = "Invalid username or password.", StatusCode = 401 };

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid) return new AuthenticationResponse { Message = "Invalid username or password.", StatusCode = 401 };

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user, roles[0]);
        var refreshToken = _tokenService.GenerateRefreshToken();
        await UpdateUsersRefreshToken(user, refreshToken);

        //var message = new EmailMessage(new string[] { "adrian.bajczyk@gmail.com" }, "Test email async", "This is the content from our async email.");
        // _emailSender.SendEmail(message);

        return new AuthenticationResponse { Message = "Login succeed.", StatusCode = 200, Token = accessToken, RefreshToken = refreshToken };
    }

    private async Task UpdateUsersRefreshToken(User user, string refreshToken)
    {
        if (user.RefreshTokenModel == null)
        {

            user.RefreshTokenModel = new RefreshTokenModel
            {
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7),
            };
        }
        else
        {
            user.RefreshTokenModel.RefreshToken = refreshToken;
            user.RefreshTokenModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        }

        await _dbContext.SaveChangesAsync();
    }
}
