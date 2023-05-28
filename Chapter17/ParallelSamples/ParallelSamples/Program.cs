// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;

ParallelFor();
Console.WriteLine();

ParallelForWithAsync();
Console.WriteLine();

StopParallelForEarly();
Console.WriteLine();

ParallelForWithInit();
Console.WriteLine();

ParallelForEach();
Console.WriteLine();

ParallelInvoke();

static void ParallelInvoke()
{
    Console.WriteLine(nameof(ParallelInvoke));

    Parallel.Invoke(Foo, Bar, Foo, Bar, Foo, Bar);

    void Foo() => Console.WriteLine("foo");
    void Bar() => Console.WriteLine("bar");
}

static void ParallelForEach()
{
    string[] data =
    {
        "zero",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
        "ten",
        "eleven",
        "twelve",
    };

    Console.WriteLine(nameof(ParallelForEach));
    ParallelLoopResult result = Parallel.ForEach<string>(data, s => Console.WriteLine(s));
}

static void ParallelForWithInit()
{
    Console.WriteLine(nameof(ParallelForWithInit));

    _ = Parallel.For<string>(0, 10, () =>
    {
        Log($"init thread");
        return $"{Environment.CurrentManagedThreadId}";
    },
    (i, pls, str1) =>
    {
        Log($"body i {i} str1 {str1}");
        Task.Delay(10).Wait();
        return $"i {i}";
    },
    (str1) => Log($"finally {str1}"));
}

static void StopParallelForEarly()
{
    ParallelLoopResult result = Parallel.For(10, 40, (int i, ParallelLoopState pls) =>
    {
        Log($"S {i}");
        if (i > 12)
        {
            pls.Break();
            Log($"break now... {i}");
        }
        Task.Delay(10).Wait();
        Log($"E {i}");
    });

    Console.WriteLine($"Is completed: {result.IsCompleted}");
    Console.WriteLine($"lowest break iteration: {result.LowestBreakIteration}");
}

static void ParallelForWithAsync()
{
    Console.WriteLine(nameof(ParallelForWithAsync));

    bool[] completions = Enumerable.Repeat(false, 10).ToArray();
    ParallelLoopResult result = Parallel.For(0, 10, async i =>
    {
        Log($"S {i}");
        await Task.Delay(10);
        Log($"E {i}");
        completions[i] = true;
    });

    while (completions.Any(b => b is false)) { }
    Console.WriteLine($"Is completed: {result.IsCompleted}");
}

static void ParallelFor()
{
    Console.WriteLine(nameof(ParallelFor));

    ParallelLoopResult result = Parallel.For(0, 10, i =>
    {
        Log($"S {i}");
        Task.Delay(10).Wait();
        Log($"E {i}");
    });

    Console.WriteLine($"Is completed: {result.IsCompleted}");
}

static void Log(string prefix) => Console.WriteLine($"{prefix}, task: {Task.CurrentId}, " +
    $"thread: {Environment.CurrentManagedThreadId}");
