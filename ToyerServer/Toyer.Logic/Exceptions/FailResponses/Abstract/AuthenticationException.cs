namespace Toyer.Logic.Exceptions.FailResponses.Abstract;

public abstract class AuthenticationException : Exception
{
    protected AuthenticationException(string message) : base(message)
    {
    }
}