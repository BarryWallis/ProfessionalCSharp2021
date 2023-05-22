// See https://aka.ms/new-console-template for more information

using LoggingSample;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using OpenTelemetry.Logs;

using System.Runtime.InteropServices;

IHost host = Host.CreateDefaultBuilder(args)
                        .ConfigureLogging((context, logging) =>
                        {
                            _ = logging.ClearProviders();
                            _ = logging.AddConsole()
                                       .AddDebug()
                                       .AddEventSourceLogger()
                                       .AddOpenTelemetry(options
                                                         => options.AddConsoleExporter());

                            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                            if (isWindows)
                            {
                                //_ = logging.AddEventLog();
                                _ = logging.AddFilter<EventLogLoggerProvider>(level => level
                                                                                       >= LogLevel.Warning);
                            }
                        })
                        .ConfigureServices(services =>
                        {
                            _ = services.AddHttpClient<NetworkService>(client =>
                                {

                                }).AddTypedClient<NetworkService>();
                            ILogger logger = new LoggerFactory().CreateLogger("test");
                            _ = services.AddScoped<Runner>()
                                        .AddSingleton<ILogger>(logger);
                        }).Build();


Runner runner = host.Services.GetRequiredService<Runner>();
await runner.RunAsync();
Console.WriteLine("Bye... Press return to exit");

