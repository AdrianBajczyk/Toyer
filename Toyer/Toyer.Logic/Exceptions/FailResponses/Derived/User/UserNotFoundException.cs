using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.User;

public sealed class UserNotFoundException(string userId)
        : NotFoundException($"User with the identifier {userId} was not found.");
