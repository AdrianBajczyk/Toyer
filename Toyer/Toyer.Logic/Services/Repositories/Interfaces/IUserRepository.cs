using Microsoft.AspNetCore.Identity;
using Toyer.Data.Entities;
using Toyer.Logic.Responses;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IUserRepository
{
   Task<IdentityResult> RegisterNewUserAsync(User newUser, string password);
   Task<AuthorizationResponse> LoginAsync(string email, string password);
   Task<User?> GetUserByIdAsync(string Id);
   Task<List<User>?> GetUsersAsync();
   Task<Address?> UpdateAddressAsync(string userId, Address updatesFromUserDocument);
   Task<PersonalInfo?> UpdatePersonalInfoPatchAsync(string userId, PersonalInfo updatesFromUser);
   Task<IdentityResult?> DeleteUserAsync(string Id);
   Task<IdentityResult> UpdateContactInfoAsync(string userId, string? email, string? phoneNumber);
}
