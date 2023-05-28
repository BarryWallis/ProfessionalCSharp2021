// See https://aka.ms/new-console-template for more information

CancelParallelFor();
CancelTask();

static void CancelTask()
{
    Console.WriteLine(nameof(CancelTask));

    CancellationTokenSource cancellationTokenSource = new(millisecondsDelay: 500);

    _ = cancellationTokenSource.Token.Register(() => Console.WriteLine("*** task cancelled"));
    Task task1 = Task.Run(() =>
    {
        try
        {
            Console.WriteLine("in task");
            for (int i = 0; i < 20; i++)
            {
                Task.Delay(100).Wait();
                CancellationToken cancellationToken = cancellationTokenSource.Token;
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine($"cancelling was requested, cancelling from within the task");
                    cancellationToken.ThrowIfCancellationRequested();
                    break;
                }
                Console.WriteLine("in loop");
            }
            Console.WriteLine("task finished without cancellation");
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine($"exception: {ex.GetType().Name}, {ex.Message}");
            //foreach (Exception innerException in ex.InnerExceptions)
            //{
            //    Console.WriteLine($"inner exception: {ex.InnerException?.GetType()}, {ex.InnerException?.Message}");
            //}
        }
    }, cancellationTokenSource.Token);
    task1.Wait();

    Console.WriteLine();
}

static void CancelParallelFor()
{
    Console.WriteLine(nameof(CancelParallelFor));

    CancellationTokenSource cancellationTokenSource = new(millisecondsDelay: 500);
    _ = cancellationTokenSource.Token.Register(() => Console.WriteLine("*** cancellation activated"));
    try
    {
        ParallelLoopResult parallelLoopResult = Parallel.For(0, 100,
            new ParallelOptions { CancellationToken = cancellationTokenSource.Token, }, x =>
        {
            Console.WriteLine($"loop {x} started");
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                Task.Delay(2).Wait();
                sum += 1;
            }
            Console.WriteLine($"loop {x} finished");
        });
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine(ex.Message);
    }

    Console.WriteLine();
}
