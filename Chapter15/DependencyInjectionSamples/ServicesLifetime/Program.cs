// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using ServicesLifetime;

if (args.Length != 1)
{
    Usage();
    return;
}

switch (args[0].ToLower())
{
    case "singletonandtransient":
        SingletonAndTransient();
        break;
    case "scoped":
        UsingScoped();
        break;
    case "custom":
        CustomFactories();
        break;
    default:
        Usage();
        break;
}

static void Usage() => Console.WriteLine("usage: ServicesLifetime [singletonandtransient|scoped|custom]");

void CustomFactories()
{
    IServiceB CreateServiceBFactory(IServiceProvider provider)
        => new ServiceB(provider.GetRequiredService<INumberService>(),
                        provider.GetRequiredService<IOptions<ConfigurationB>>());

    Console.WriteLine(nameof(CustomFactories));

    using IHost host = Host.CreateDefaultBuilder().ConfigureServices(services =>
    {
        NumberService numberService = new();
        _ = services.AddSingleton<INumberService>(numberService)
                    .Configure<ConfigurationB>(config => config.Mode = "factory")
                    .AddTransient<IServiceB>(CreateServiceBFactory)
                    .Configure<ConfigurationA>(config => config.Mode = "singleton")
                    .AddSingleton<IServiceA, ServiceA>();
    }).Build();

    IServiceA a1 = host.Services.GetRequiredService<IServiceA>();
    IServiceA a2 = host.Services.GetRequiredService<IServiceA>();
    IServiceB b1 = host.Services.GetRequiredService<IServiceB>();
    IServiceB b2 = host.Services.GetRequiredService<IServiceB>();
    Console.WriteLine();
}

void UsingScoped()
{
    Console.WriteLine(nameof(UsingScoped));

    using IHost host = Host.CreateDefaultBuilder().ConfigureServices(services
        => _ = services.AddSingleton<INumberService, NumberService>()
                       .Configure<ConfigurationA>(config => config.Mode = "scoped")
                       .AddScoped<IServiceA, ServiceA>()
                       .Configure<ConfigurationB>(config => config.Mode = "singleton")
                       .AddSingleton<IServiceB, ServiceB>()
                       .Configure<ConfigurationC>(config => config.Mode = "transient")
                       .AddTransient<IServiceC, ServiceC>()).Build();

    using (IServiceScope scope1 = host.Services.CreateScope())
    {
        IServiceA a1 = scope1.ServiceProvider.GetRequiredService<IServiceA>();
        a1.A();
        IServiceA a2 = scope1.ServiceProvider.GetRequiredService<IServiceA>();
        a2.A();
        IServiceB b1 = scope1.ServiceProvider.GetRequiredService<IServiceB>();
        b1.B();
        IServiceC c1 = scope1.ServiceProvider.GetRequiredService<IServiceC>();
        c1.C();
        IServiceC c2 = scope1.ServiceProvider.GetRequiredService<IServiceC>();
        c2.C();
    }
    Console.WriteLine("end of scope1");

    using (IServiceScope scope2 = host.Services.CreateScope())
    {
        IServiceA a3 = scope2.ServiceProvider.GetRequiredService<IServiceA>();
        a3.A();
        IServiceB b2 = scope2.ServiceProvider.GetRequiredService<IServiceB>();
        b2.B();
        IServiceC c3 = scope2.ServiceProvider.GetRequiredService<IServiceC>();
        c3.C();
    }
    Console.WriteLine("end of scope2");

    Console.WriteLine();
}

static void SingletonAndTransient()
{
    Console.WriteLine(nameof(SingletonAndTransient));

    using IHost host = Host.CreateDefaultBuilder().ConfigureServices(services
        => _ = services.Configure<ConfigurationA>(config => config.Mode = "singleton")
                       .AddSingleton<IServiceA, ServiceA>()
                       .Configure<ConfigurationB>(config => config.Mode = "transient")
                       .AddTransient<IServiceB, ServiceB>()
                       .AddTransient<ControllerX>()
                       .AddSingleton<INumberService, NumberService>()).Build();

    Console.WriteLine($"requesting {nameof(ControllerX)}");
    ControllerX x = host.Services.GetRequiredService<ControllerX>();
    x.M();
    x.M();

    Console.WriteLine($"requesting {nameof(ControllerX)}");
    ControllerX x2 = host.Services.GetRequiredService<ControllerX>();
    x2.M();

    Console.WriteLine();
}
