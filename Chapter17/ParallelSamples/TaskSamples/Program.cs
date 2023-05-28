// See https://aka.ms/new-console-template for more information

using System.Reflection.Metadata.Ecma335;

internal class Program
{
    private static readonly object _logLock = new();

    private static void Main()
    {
        TasksUsingThreadPool();
        RunSynchronousTask();
        LongRunningTask();
        TaskWithResultDemo();
        ContinuationTasks();
        ParentAndChild();
     }

    public static void ParentAndChild()
    {
        Task parent = new(ParentTask);
        parent.Start();
        Task.Delay(2000).Wait();
        Console.WriteLine(parent.Status);
        Task.Delay(4000).Wait();
        Console.WriteLine(parent.Status);
    }

    public static void ParentTask()
    {
        Console.WriteLine($"task id {Task.CurrentId}");
        Task child = new(ChildTask);
        child.Start();
        Task.Delay(1000).Wait();
        Console.WriteLine($"parent started child");
    }

    public static void ChildTask()
    {
        Console.WriteLine("child");
        Task.Delay(5000).Wait();
        Console.WriteLine($"child finished");
    }

    public static void ContinuationTasks()
    {
        Task task1 = new(DoOnFirst);
        Task task2 = task1.ContinueWith(DoOnSecond);
        Task task3 = task1.ContinueWith(DoOnSecond);
        Task task4 = task2.ContinueWith(DoOnSecond);
        task1.Start();
        Task.WaitAll(new Task[] { task1, task2, task3, task4 });
        Console.WriteLine();
    }

    private static void DoOnSecond(Task task)
    {
        Console.WriteLine($"task {task.Id} finished");
        Console.WriteLine($"this task id {Task.CurrentId}");
        Console.WriteLine($"do some cleanup");
        Task.Delay(3000).Wait();
    }

    private static void DoOnFirst()
    {
        Console.WriteLine($"doing some task {Task.CurrentId}");
        Task.Delay(3000).Wait();
    }

    public static void TaskWithResultDemo()
    {
        Task<(int Result, int Remainder)> t1 = new(TaskWithResult, (8, 3));
        t1.Start();
        Console.WriteLine($"result from task: {t1.Result.Result}, and remainder: {t1.Result.Remainder}");
        Console.WriteLine();
    }

    private static (int Result, int Remainder) TaskWithResult(object? division)
    {
        if (division is ValueTuple<int, int> div)
        {
            (int x, int y) = div;
            int result = x / y;
            int remainder = x % y;
            Console.WriteLine("task creates a result...");

            return (result, remainder);
        }
        else
        {
            throw new ArgumentException($"{nameof(division)} needs to be a ValueTuiple<int, int>");
        }
    }

    public static void LongRunningTask()
    {
        Task task1 = new(TaskMethod!, "long running", TaskCreationOptions.LongRunning);
        task1.Start();
    }

    public static void RunSynchronousTask()
    {
        TaskMethod("Just the main thread");
        Task task1 = new(TaskMethod!, "run sync");
        task1.RunSynchronously();
    }

    public static void TasksUsingThreadPool()
    {
        TaskFactory taskFactory = new();
        Task task1 = taskFactory.StartNew(TaskMethod!, "using a task factory");
        Task task2 = Task.Factory.StartNew(TaskMethod!, "factory via a task");
        Task task3 = new(TaskMethod!, "using a task constructor and Start");
        task3.Start();
        Task task4 = Task.Run(() => TaskMethod("using the Run method"));
        Task.WaitAll(new Task[] { task1, task2, task3, task4 });
    }

    public static void TaskMethod(object o) => Log(o?.ToString() ?? string.Empty);

    public static void Log(string title)
    {
        lock (_logLock)
        {
            Console.WriteLine(title);
            Console.WriteLine($"Task id: {Task.CurrentId?.ToString() ?? "no task"}, " +
                $"thread: {Environment.CurrentManagedThreadId}");
            Console.WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"is background thread: {Thread.CurrentThread.IsBackground}");
            Console.WriteLine();
        }
    }
}
