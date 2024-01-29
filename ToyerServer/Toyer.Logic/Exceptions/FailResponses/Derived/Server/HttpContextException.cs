using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.Server;

public sealed class HttpContextException(string message)
        : InternalServerErrorException(message);
