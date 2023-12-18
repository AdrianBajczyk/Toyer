﻿using Microsoft.AspNetCore.JsonPatch;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Services.Repositories.Interfaces;

public interface IUserRepository
{
   public Task<User?> CreateNewUserAsync(User newUser);
   public Task<User?> GetUserByIdAsync(Guid Id);
   public Task<Address?> UpdateAddressPatchAsync(Guid userId, Address updatesFromUserDocument);
   public Task<PersonalInfo?> UpdatePersonalInfoPatchAsync(Guid userId, PersonalInfo updatesFromUser);
   public Task<User?> AssociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
   public Task<User?> UnassociateDeviceWithAccAsync(DeviceAPConnectionDto deviceAp, UserPasswordChangeDto userLogin);
}
