// See https://aka.ms/new-console-template for more information

namespace SolicitColdCall;

public class SalesSpyFoundException : Exception
{
    public SalesSpyFoundException(string spyName) : base($"Sales spy found with name {spyName}")
    {
    }

    public SalesSpyFoundException(string spyName, Exception? innerException) : base(spyName, innerException)
    {
    }
}
