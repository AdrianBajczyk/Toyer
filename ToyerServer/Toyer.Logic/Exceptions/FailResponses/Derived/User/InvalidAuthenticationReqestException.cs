using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.User;

public sealed class InvalidAuthenticationReqestException()
        : BadRequestException($"Invalid authentication reqest.");
