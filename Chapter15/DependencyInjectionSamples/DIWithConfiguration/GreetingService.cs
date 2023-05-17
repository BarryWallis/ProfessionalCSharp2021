using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

namespace DIWithConfiguration;
public class GreetingService : IGreetingService
{
    private readonly string? _from;

    public GreetingService(IOptions<GreetingServiceOptions> options) => _from = options.Value.From;

    public string Greet(string name) => $"Hello, {name}! Greetings from {_from}";
}
