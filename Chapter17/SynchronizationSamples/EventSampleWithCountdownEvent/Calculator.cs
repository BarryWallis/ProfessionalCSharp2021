using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EventSampleWithCountdownEvent;

public class Calculator
{
    private readonly CountdownEvent _event;

    public Calculator(CountdownEvent anEvent) => _event = anEvent;

    public int Result { get; private set; }

    public void Calculation(int x, int y)
    {
        Console.WriteLine($"Task {Task.CurrentId} starts calculation");
        Task.Delay(new Random().Next(3000)).Wait();
        Result = x + y;
        Console.WriteLine($"Task {Task.CurrentId} is ready");
        _ = _event.Signal();
    }
}
