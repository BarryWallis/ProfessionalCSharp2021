using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingIssues;

public class TaskWithDeadlock
{
    private readonly StateObject _stateObject1;
    private readonly StateObject _stateObject2;

    public TaskWithDeadlock(StateObject stateObject1, StateObject stateObject2)
    {
        _stateObject1 = stateObject1;
        _stateObject2 = stateObject2;
    }

    public void Deadlock1()
    {
        int i = 0;
        while (true)
        {
            lock(_stateObject1)
            {
                lock (_stateObject2)
                {
                    _ = _stateObject1.ChangeState(i);
                    _ = _stateObject2.ChangeState(i++);
                    Console.WriteLine($"still running, {i}");
                }
            }
        }
    }

    public void Deadlock2()
    {
        int i = 0;
        while (true)
        {
            lock (_stateObject2)
            {
                lock (_stateObject1)
                {
                    _ = _stateObject1.ChangeState(i);
                    _ = _stateObject2.ChangeState(i++);
                    Console.WriteLine($"still running, {i}");
                }
            }
        }
    }
}
