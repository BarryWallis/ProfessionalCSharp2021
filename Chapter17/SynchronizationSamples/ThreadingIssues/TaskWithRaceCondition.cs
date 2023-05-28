using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingIssues;

public class TaskWithRaceCondition
{
#pragma warning disable CA1822 // Mark members as static
    public void RaceCondition(object o)
#pragma warning restore CA1822 // Mark members as static
    {
        if (o is not StateObject stateObject)
        {
            throw new ArgumentException($"{nameof(o)} must be a {nameof(stateObject)}");
        }
        else
        {
            Console.WriteLine("starting race condition - when does the issue occur?");

            int i = 0;
            while (true)
            {
                if (!stateObject.ChangeState(i++))
                {
                    i = 0;
                }
            }
        }
    }
}
