using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

namespace ServicesLifetime;

public class ConfigurationC
{
    public string? Mode { get; set; }
}

public sealed class ServiceC : IServiceC, IDisposable
{
    private readonly int _n;
    private readonly string? _mode;

    public ServiceC(INumberService numberService, IOptions<ConfigurationC> options)
    {
        _mode = options.Value.Mode;
        _n = numberService.GetNumber();
        Console.WriteLine($"ctor {nameof(ServiceC)}, {_n}");
    }

    public void C() => Console.WriteLine($"{nameof(C)}, {_n}, mode: {_mode}");

    public void Dispose() => Console.WriteLine($"disposing {nameof(ServiceC)}, {_n}");
}
