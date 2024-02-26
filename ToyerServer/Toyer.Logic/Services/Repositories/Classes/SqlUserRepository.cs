using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Logic.Exceptions.FailResponses.Derived.Server;
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
    private readonly IHttpContextAccessor _httpAccessor;
    private readonly LinkGenerator _generator;

    public SqlUserRepository(UsersDbContext usersDbContext, UserManager<User> userManager, ITokenService tokenService, IEmailSender emailSender, IHttpContextAccessor httpAccessor, LinkGenerator generator)
    {
        _dbContext = usersDbContext;
        _userManager = userManager;
        _tokenService = tokenService;
        _emailSender = emailSender;
        _httpAccessor = httpAccessor;
        _generator = generator;
    }
    public async Task<User> GetUserByIdAsync(string userId)
    {
        return await _dbContext.Users
                    .Include(u => u.PersonalInfo)
                    .ThenInclude(p => p.Address)
                    .SingleOrDefaultAsync(u => u.Id.ToString() == userId)
                    ?? throw new UserNotFoundException(userId);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var users = await _dbContext.Users
                .Include(u => u.PersonalInfo)
                .ThenInclude(p => p.Address)
                .AsNoTracking()
                .ToListAsync();

        return users.IsNullOrEmpty()
            ? throw new UsersTableEmptyException()
            : users;
    }

    public async Task<User> RegisterNewUserAsync(User newUser, string password, string redirectUrl)
    {
        var existingUser = await _userManager.FindByEmailAsync(newUser.Email); 
        if(existingUser != null) throw new UserEmailTakenException(); 

        await _userManager.CreateAsync(newUser);

        await _userManager.AddToRoleAsync(newUser, "RegisteredUser");
        await _userManager.AddPasswordAsync(newUser, password);
        await SendConfimationLinkByEmailAsync(newUser, redirectUrl);

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

        

        if (!string.IsNullOrWhiteSpace(email)) 
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null) throw new UserEmailTakenException();
            await _userManager.SetEmailAsync(userToUpdate, email);
        }
        if (!string.IsNullOrWhiteSpace(phoneNumber)) await _userManager.SetPhoneNumberAsync(userToUpdate, phoneNumber);

         await _userManager.UpdateAsync(userToUpdate);
    }

    public async Task<(string ,AuthenticationResponse)> LoginAsync(string email, string password)
    {
        var user = await _userManager.Users.Include(u => u.RefreshTokenModel).FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new InvalidUserOrPasswordException();

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid) throw new InvalidUserOrPasswordException();

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user, (List<string>)roles);
        var refreshToken = _tokenService.GenerateRefreshToken();
        await UpdateUsersRefreshToken(user, refreshToken);

        if (!user.EmailConfirmed) throw new EmailNotConfirmedException();

        return (refreshToken, new AuthenticationResponse()
        {
            Id = user.Id,
            Status = 200,
            Message = "Success",
            Token = accessToken,
            Roles = roles,
        });
    }

    private async Task UpdateUsersRefreshToken(User user, string refreshToken)
    {
        if (user.RefreshTokenModel == null)
        {
            user.RefreshTokenModel = new RefreshTokenModel {
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

    public async Task ConfirmEmailAsync(string token, string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail) ?? throw new UserNotFoundException(userEmail);
        if (user.EmailConfirmed) throw new EmailAlreadyConfirmedException();
        
        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded) throw new EmailServiceException("Unexpected server error occured.");

    }

    public async Task ResendEmailConfirmationLink(string userEmail, string redirectUrl)
    {
        var user = await _userManager.FindByEmailAsync(userEmail) 
            ?? throw new UserNotFoundException("userEmail");


        await SendConfimationLinkByEmailAsync(user, redirectUrl);
    }

    public async Task SendPasswordResetLink(string email)
    {
        var user = await _userManager.FindByEmailAsync(email)
            ?? throw new UserNotFoundException(email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var confirmationLink = _generator.GetUriByAction(_httpAccessor.HttpContext, action: "ResetPassword", controller: "User", values: new { token, email = user.Email }) 
            ?? throw new HttpContextException("Email confirmation link error.");

        var message = new EmailMessage(new string[] { user.Email }, "Password reset link", confirmationLink);
        await _emailSender.SendEmailAsync(message);
    }

    private async Task SendConfimationLinkByEmailAsync(User user, string redirectUrl)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmationLink = _generator.GetUriByAction(_httpAccessor.HttpContext, action: "ConfirmEmail", controller: "User", values: new { token, email = user.Email, redirectUrl }) 
            ?? throw new HttpContextException("Email confirmation link error.");

        var message = new EmailMessage(new string[] { user.Email }, "Confirmation email link", confirmationLink);
        await _emailSender.SendEmailAsync(message);
    }
}
