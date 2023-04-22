namespace DefaultInterfaceMethods;
public class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine(message);

    void ILogger.Log(Exception exception)
        => Console.WriteLine($"exception type: {exception.GetType().Name}, message: {exception.Message}");
}
