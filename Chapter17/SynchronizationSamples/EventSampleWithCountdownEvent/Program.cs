// See https://aka.ms/new-console-template for more information

using EventSampleWithCountdownEvent;

const int taskCount = 4;

CountdownEvent theEvent = new(taskCount);
Calculator[] calculators = new Calculator[taskCount];

for (int i = 0; i < taskCount; i++)
{
    calculators[i] = new(theEvent);
    int i1 = i;
    _ = Task.Run(() => calculators[i1].Calculation(i1 + 1, i1 + 3));
}
theEvent.Wait();

Console.WriteLine("all finished");
for (int i = 0; i < taskCount; i++)
{
    Console.WriteLine($"task for {i}, result: {calculators[i].Result}");
}
