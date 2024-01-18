using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.Server;

public sealed class EmailServiceException(string message)
        : InternalServerErrorException(message);