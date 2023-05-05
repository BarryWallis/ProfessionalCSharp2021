namespace RethrowExceptions;

public class MyCustomException : Exception
{
    public int ErrorCode { get; set; }

    public MyCustomException(string message) : base(message)
    {
    }
}

public class AnotherCustomException : Exception
{
    public AnotherCustomException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
