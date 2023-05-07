// See https://aka.ms/new-console-template for more information
CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(5));

try
{
    await RunTaskAsync(cancellationTokenSource.Token);
}
catch (OperationCanceledException ex)
{
    Console.WriteLine(ex.Message);
}

Task RunTaskAsync(CancellationToken cancellationToken) => Task.Run(async () =>
{
    while (true)
    {
        Console.WriteLine(".");
        await Task.Delay(100);
        if (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("resource cleanup and good bye!");
            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}, cancellationToken);
