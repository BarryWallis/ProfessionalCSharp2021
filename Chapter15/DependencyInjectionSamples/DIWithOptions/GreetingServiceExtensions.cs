using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace DIWithOptions;

public static class GreetingServiceExtensions
{
    public static IServiceCollection AddGreetingService(this IServiceCollection services,
                                                        Action<GreetingServiceOptions> setupAction) 
        => services.Configure(setupAction)
                   .AddTransient<IGreetingService, GreetingService>();
}
