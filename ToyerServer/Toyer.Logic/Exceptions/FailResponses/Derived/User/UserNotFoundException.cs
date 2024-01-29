using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.User;

public sealed class UserNotFoundException(string credit)
        : NotFoundException($"User {credit} was not found.");
