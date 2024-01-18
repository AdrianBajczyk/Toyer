namespace Toyer.Logic.Exceptions.FailResponses.Abstract;

public abstract class InternalServerErrorException : Exception
{
    protected InternalServerErrorException(string message) : base(message)
    {
    }
}