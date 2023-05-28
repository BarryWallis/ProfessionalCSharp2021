// See https://aka.ms/new-console-template for more information

using EventSample;

const int taskCount = 4;
ManualResetEventSlim[] events = new ManualResetEventSlim[taskCount];
WaitHandle[] waitHandles = new WaitHandle[taskCount];
Calculator[] calculators = new Calculator[taskCount];

for (int i = 0; i < taskCount; i++)
{
    int i1 = i;
    events[i] = new(initialState: false);
    waitHandles[i] = events[i].WaitHandle;
    calculators[i] = new(events[i]);
    _ = Task.Run(() => calculators[i1].Calculation(i1 + 1, i1 + 3));
}

for (int i = 0; i < taskCount; i++)
{
    int index = WaitHandle.WaitAny(waitHandles);
    if (index == WaitHandle.WaitTimeout)
    {
        Console.WriteLine("Timeout!");
    }
    else
    {
        events[index].Reset();
        Console.WriteLine($"finished task for {index}, result: {calculators[index].Result}");
    }
}
