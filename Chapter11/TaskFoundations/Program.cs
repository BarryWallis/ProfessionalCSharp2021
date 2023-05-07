// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

Dictionary<string, string> names = new();

//CallerWithAsync();
//Console.ReadLine();

//CallerWithAwaiter();
//Console.ReadLine();

//CallerWithContinuationTask();
//Console.ReadLine();

//MultipleAsyncMethods();
//Console.ReadLine();

//MultipleAsyncMethodsWithCombinators1();
//Console.ReadLine();

//MultipleAsyncMethodsWithCombinators2();
//Console.ReadLine();

//UseValueTask();
//Console.ReadLine();

UseValueTask2();
Console.ReadLine();

#pragma warning disable CS8321 // Local function is declared but never used
async void UseValueTask2()
{
    string result = await GreetingValueTask2Async("Katharina");
    Console.WriteLine(result);
    string result2 = await GreetingValueTask2Async("Katharina");
    Console.WriteLine(result2);
}

async void UseValueTask()
{
    string result = await GreetingValueTaskAsync("Katharina");
    Console.WriteLine(result);
    string result2 = await GreetingValueTaskAsync("Katharina");
    Console.WriteLine(result2);
}

ValueTask<string> GreetingValueTask2Async(string name)
{

    if (names.TryGetValue(name, out string? result))
    {
        return new(result);
    }
    else
    {
        Task<string> t1 = GreetingAsync(name);
        TaskAwaiter<string> taskAwaiter = t1.GetAwaiter();
        taskAwaiter.OnCompleted(OnCompletion);
        return new(t1);

        void OnCompletion() => names.Add(name, taskAwaiter.GetResult());
    }
}


async ValueTask<string> GreetingValueTaskAsync(string name)
{

    if (names.TryGetValue(name, out string? result))
    {
        return result;
    }
    else
    {
        result = await GreetingAsync(name);
        names.Add(name, result);
        return result;
    }
}

async void MultipleAsyncMethodsWithCombinators2()
{
    Task<string> t1 = GreetingAsync("Stephanie");
    Task<string> t2 = GreetingAsync("Matthias");
    string[] result = await Task.WhenAll(t1, t2);
}

async void MultipleAsyncMethodsWithCombinators1()
{
    Task<string> t1 = GreetingAsync("Stephanie");
    Task<string> t2 = GreetingAsync("Matthias");
    _ = await Task.WhenAll(t1, t2);
    Console.WriteLine($"Finished both methods.");
    Console.WriteLine($"Result 1: {t1.Result}");
    Console.WriteLine($"Result 2: {t2.Result}");
}

async void MultipleAsyncMethods()
{
    string s1 = await GreetingAsync("Stephanie");
    string s2 = await GreetingAsync("Matthias");
    Console.WriteLine($"Finished both methods.");
    Console.WriteLine($"Result 1: {s1}");
    Console.WriteLine($"Result 2: {s2}");
}

void CallerWithContinuationTask()
{
    TraceThreadAndTask($"started {nameof(CallerWithContinuationTask)}");
    Task<string> t1 = GreetingAsync("Stephanie");
    _ = t1.ContinueWith(t =>
    {
        string result = t.Result;
        Console.WriteLine(result);
        TraceThreadAndTask($"ended {nameof(CallerWithContinuationTask)}");
    });
}

void TraceThreadAndTask(string info)
{
    string taskInfo = Task.CurrentId is null ? "no task" : $"task {Task.CurrentId}";
    Console.WriteLine($"{info} in thread {Environment.CurrentManagedThreadId} and {taskInfo}");
}

string Greeting(string name)
{
    TraceThreadAndTask($"running {nameof(Greeting)}");
    Task.Delay(3000).Wait();
    return $"Hello, {name}";
}

Task<string> GreetingAsync(string name) => Task.Run(() =>
{
    TraceThreadAndTask($"running {nameof(GreetingAsync)}");
    return Greeting(name);
});

async void CallerWithAsync()
{
    TraceThreadAndTask($"started {nameof(CallerWithAsync)}");
    string result = await GreetingAsync("Stephanie");
    Console.WriteLine(result);
    TraceThreadAndTask($"ended {nameof(CallerWithAsync)}");
}

void CallerWithAwaiter()
{
    TraceThreadAndTask($"starting {nameof(CallerWithAwaiter)}");
    TaskAwaiter<string> taskAwaiter = GreetingAsync("Matthias").GetAwaiter();
    taskAwaiter.OnCompleted(OnCompleteAwaiter);

    void OnCompleteAwaiter()
    {
        Console.WriteLine(taskAwaiter.GetResult());
        TraceThreadAndTask($"ended {nameof(CallerWithAwaiter)}");
    }
}
#pragma warning restore CS8321 // Local function is declared but never used

