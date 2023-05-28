using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingIssues;

public class StateObject
{
    private int _state = 5;

    public bool ChangeState(int loop)
    {
        if (_state == 5)
        {
            _state += 1;
            if (_state != 6)
            {
                Console.WriteLine($"Race condition occurred after {loop} loops");
                //Trace.Fail("race condition");
                return false;
            }
        }

        _state = 5;
        return true;
    }
}
