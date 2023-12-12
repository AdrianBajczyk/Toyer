namespace Toyer.Logic.Exceptions;


    public class NotFound : Exception
    {
        public NotFound(string message) : base(message) { }
    }

    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception innerException) : base(message, innerException) { }
    }

