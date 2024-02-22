using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.User;

public sealed class UserHasNoDevicesException(string userId) : NotFoundException($"User with identifier {userId} hasn't any device yet.");