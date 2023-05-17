using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLifetime;
public sealed class ControllerX : IDisposable
{
    private readonly IServiceA _serviceA;
    private readonly IServiceB _serviceB;
    //private readonly IServiceC? _serviceC;
    private readonly int _n;
    private int _countM = 0;

    public ControllerX(IServiceA serviceA, IServiceB serviceB, INumberService numberService)
    {
        _serviceA = serviceA;
        _serviceB = serviceB;
        _n = numberService.GetNumber();
        Console.WriteLine($"ctor {nameof(ControllerX)}, {_n}");
    }

    public void M()
    {
        Console.WriteLine($"invoked {nameof(M)} for the {++_countM}. time");
        _serviceA.A();
        _serviceB.B();
    }

    public void Dispose() => Console.WriteLine($"disposing {nameof(ControllerX)}, {_n}");
}
