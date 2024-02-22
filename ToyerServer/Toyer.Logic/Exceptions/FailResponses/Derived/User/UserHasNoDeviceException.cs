using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.User;

public sealed class UserHasNoDeviceException(string userId, string deviceId)
    : NotFoundException($"User with identifier {userId} has no device with identifier {deviceId}");
