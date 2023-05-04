// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

IEnumerable<int> data = SampleData();
LinqQuery(data);
UseCancellation(data);

void UseCancellation(IEnumerable<int> data)
{
    Console.WriteLine(nameof(UseCancellation));
    using CancellationTokenSource cancellationTokenSource = new();
    Task task = Task.Run(() =>
    {
        try
        {
            double result = (from x in data.AsParallel().WithCancellation(cancellationTokenSource.Token)
                             where Math.Log(x) < 4
                             select x
                            ).Average();
            Console.WriteLine($"Query finished, sum: {result}");
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine(ex.Message);
        }
    });

    Console.Write($"Cancel? ");
    string? input = Console.ReadLine();
    if (input?.ToLower() == "y")
    {
        cancellationTokenSource.Cancel();
    }
    task.Wait();
    Console.WriteLine();
}

static void LinqQuery(IEnumerable<int> data)
{
    Console.WriteLine(nameof(LinqQuery));
    Stopwatch stopwatch = Stopwatch.StartNew();
    double result = (from x in data.AsParallel()
                     where Math.Log(x) < 4
                     select x
                 ).Average();
    stopwatch.Stop();
    Console.WriteLine($"Calculated {result} in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
    Console.WriteLine();
}

static IEnumerable<int> SampleData()
{
    Console.WriteLine(nameof(SampleData));
    const int arraySize = 500_000_000;
    Random random = new();
    Stopwatch stopwatch = Stopwatch.StartNew();
    List<int> data = Enumerable.Range(0, arraySize).Select(x => random.Next(140)).ToList();
    stopwatch.Stop();
    Console.WriteLine($"Created {arraySize} item array in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
    Console.WriteLine();
    return data;
}
