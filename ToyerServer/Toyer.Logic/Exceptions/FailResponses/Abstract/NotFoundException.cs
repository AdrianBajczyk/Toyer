namespace Toyer.Logic.Exceptions.FailResponses.Abstract;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message)
       : base(message)
    {
    }
}
