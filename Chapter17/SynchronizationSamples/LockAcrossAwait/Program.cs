using LockAcrossAwait;

internal class Program
{
    private static async Task Main() => await RunUseSemaphoreAsync();

    private static readonly AsyncSemaphore _asyncSemaphore = new();

    private static async Task UseAsyncSemaphore(string title)
    {
        using (await _asyncSemaphore.WaitAsync())
        {
            Console.WriteLine($"{title} {nameof(LockWithSemaphore)} started");
            await Task.Delay(500);
            Console.WriteLine($"{title} {nameof(LockWithSemaphore)} ending"); 
        }
    }

    private static readonly SemaphoreSlim _asyncLock = new(1);
    private static async Task LockWithSemaphore(string title)
    {
        Console.WriteLine($"{title} waiting for lock");
        await _asyncLock.WaitAsync();
        try
        {
            Console.WriteLine($"{title} {nameof(LockWithSemaphore)} started");
            await Task.Delay(500);
            Console.WriteLine($"{title} {nameof(LockWithSemaphore)} ending");
        }
        finally
        {
            _ = _asyncLock.Release();
        }
    }

    private static async Task RunUseSemaphoreAsync()
    {
        Console.WriteLine(nameof(RunUseSemaphoreAsync));

        string[] messages = { "one", "two", "three", "four", "five", "six" };
        Task[] tasks = new Task[messages.Length];
        for (int i = 0; i < messages.Length; i++)
        {
            string message = messages[i];
            tasks[i] = Task.Run(async () => await LockWithSemaphore(message));
        }

        await Task.WhenAll(tasks);
        Console.WriteLine();
    }
}
