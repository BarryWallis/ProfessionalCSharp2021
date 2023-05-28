// See https://aka.ms/new-console-template for more information

using ThreadingIssues;

string input;
do
{
    Console.Write("Enter 1 for Race Condition or 2 for Deadlock: ");
    input = Console.ReadLine() ?? string.Empty;
} while (input[0] is not ('1' or '2'));

if (input[0] == '1')
{
    RaceConditions();
}
else
{
    Deadlock();
}

void Deadlock()
{
    StateObject stateObject1 = new();
    StateObject stateObject2 = new();
    Task task1 = new(new TaskWithDeadlock(stateObject1, stateObject2).Deadlock1);
    Task task2 = new(new TaskWithDeadlock(stateObject1, stateObject2).Deadlock2);
    task1.Start();
    task2.Start();
    Task.WaitAll(task1, task2);
}

void RaceConditions()
{
    StateObject stateObject = new();
    Task[] tasks = new Task[2];
    for (int i = 0; i < 2; i++)
    {
        tasks[i] = Task.Run(() => new TaskWithRaceCondition().RaceCondition(stateObject));
    }

    Task.WaitAll(tasks);
}
