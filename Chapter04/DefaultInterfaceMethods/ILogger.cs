namespace DefaultInterfaceMethods;
public interface ILogger
{
    public void Log(string message);
    public void Log(Exception exception) => Log(exception.Message);
}
