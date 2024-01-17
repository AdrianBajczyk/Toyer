namespace Toyer.Logic.Exceptions.FailResponses.Derived.User;

public sealed class AccessException()
        : UnauthorizedAccessException($"Access denied.");
