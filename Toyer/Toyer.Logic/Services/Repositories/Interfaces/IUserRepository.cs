using Microsoft.AspNetCore.Identity;
using Toyer.Data.Entities;
using Toyer.Logic.Responses;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IUserRepository
{
   Task<User> RegisterNewUserAsync(User newUser, string password);
   Task<AuthorizationResponse> LoginAsync(string email, string password);
   Task<User> GetUserByIdAsync(string Id);
   Task<IEnumerable<User>> GetUsersAsync();
   Task UpdateAddressAsync(string userId, Address updatesFromUserDocument);
   Task UpdatePersonalInfoPatchAsync(string userId, PersonalInfo updatesFromUser);
   Task DeleteUserAsync(string Id);
   Task UpdateContactInfoAsync(string userId, string? email, string? phoneNumber);
}
