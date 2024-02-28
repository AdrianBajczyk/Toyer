using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.User;

public sealed class UsersTableEmptyException()
    : NotFoundException("There are no users yet.");

