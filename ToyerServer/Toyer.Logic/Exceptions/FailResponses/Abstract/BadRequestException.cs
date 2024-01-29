namespace Toyer.Logic.Exceptions.FailResponses.Abstract;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message)
        : base(message)
    {
    }
}
