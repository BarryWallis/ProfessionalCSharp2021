// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;

using MetricsSample;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((hostingContext, logging) =>
    {
        _ = logging.ClearProviders();
        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        if (isWindows)
        {
            // Default the EventLogLoggerProvider to warning or above
            _ = logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);
        }

        _ = logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        _ = logging.AddDebug();
        _ = logging.AddEventSourceLogger();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _ = logging.AddEventLog(); // EventLogLoggerProvider
        }
        // logging.AddConsole();
        //logging.AddSimpleConsole(config =>
        //{
        //    config.IncludeScopes = true;
        //});
        //logging.AddSystemConsole(configure =>
        //{
        //    configure.IncludeScopes = true;
        //});
        //logging.AddJsonConsole(configure =>
        //{
        //    configure.IncludeScopes = true;
        //});


        _ = logging.Configure(options => options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                                | ActivityTrackingOptions.TraceId
                                                | ActivityTrackingOptions.ParentId);
    })
    .ConfigureServices(services =>
    {
        _ = services.AddHttpClient<NetworkService>(client =>
        {
        }).AddTypedClient<NetworkService>();
        _ = services.AddScoped<Runner>();
    }).Build();

Runner runner = host.Services.GetRequiredService<Runner>();
await runner.RunAsync();

Console.WriteLine("Bye... Press return to exit");
