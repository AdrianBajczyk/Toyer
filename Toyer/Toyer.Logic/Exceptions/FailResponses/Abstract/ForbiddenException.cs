namespace Toyer.Logic.Exceptions.FailResponses.Abstract;

public sealed class ForbiddenException() : Exception("Access forbidden. Request should not be repeated. It won't help. Contact support for more information.")
{
}