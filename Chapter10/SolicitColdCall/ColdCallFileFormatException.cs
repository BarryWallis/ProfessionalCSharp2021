// See https://aka.ms/new-console-template for more information

internal class ColdCallFileFormatException : Exception
{
    public ColdCallFileFormatException(string? message) : base(message)
    {
    }

    public ColdCallFileFormatException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
