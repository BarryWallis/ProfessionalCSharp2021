// See https://aka.ms/new-console-template for more information

//DontHandle();
//Console.ReadLine();

//HandleOnError();
//Console.ReadLine();

//StartTwoTasks();
//Console.ReadLine();

//StartTwoTasksParallel();
//Console.ReadLine();

using System.Diagnostics;
using System.Xml.Linq;

ShowAggregatedException();
Console.ReadLine();

#pragma warning disable CS8321 // Local function is declared but never used
static async void ShowAggregatedException()
{
    Task? taskResult = null;
    try
    {
        Task t1 = ThrowAfter(2000, "first");
        Task t2 = ThrowAfter(1000, "second");
        await (taskResult = Task.WhenAll(t1, t2));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"handled {ex.Message}");
        Debug.Assert(taskResult is not null);
        Debug.Assert(taskResult.Exception is not null);
        foreach (Exception ex1 in taskResult.Exception.InnerExceptions)
        {
            Console.WriteLine($"inner exception {ex1.Message}");
        }
    }
}

static async void StartTwoTasksParallel()
{
    try
    {
        Task t1 = ThrowAfter(2000, "first");
        Task t2 = ThrowAfter(1000, "second");
        await Task.WhenAll(t1, t2);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"handled {ex.Message}");
    }
}

static async void StartTwoTasks()
{
    try
    {
        await ThrowAfter(2000, "first");
        await ThrowAfter(1000, "second");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"handled {ex.Message}");
    }
}

static async void HandleOnError()
{
    try
    {
        await ThrowAfter(2000, "first");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void DontHandle()
{
    try
    {
        _ = ThrowAfter(2000, "first");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
#pragma warning restore CS8321 // Local function is declared but never used

static async Task ThrowAfter(int milliseconds, string message)
{
    await Task.Delay(milliseconds);
    throw new Exception(message);
}
