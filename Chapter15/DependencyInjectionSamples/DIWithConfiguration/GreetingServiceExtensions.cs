using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DIWithConfiguration;

public static class GreetingServiceExtensions
{
    public static IServiceCollection AddGreetingService(this IServiceCollection services, IConfiguration config)
        => services.Configure<GreetingServiceOptions>(config)
                   .AddTransient<IGreetingService, GreetingService>();
}
