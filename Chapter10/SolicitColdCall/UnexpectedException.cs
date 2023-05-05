// See https://aka.ms/new-console-template for more information

internal class UnexpectedException : Exception
{
    public UnexpectedException(string? message) : base(message)
    {
    }

    public UnexpectedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
