// See https://aka.ms/new-console-template for more information

using ConfigurationSample;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(config 
    => _ = config.SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("customconfigurationfile.json", optional: true))
                 .ConfigureServices(services 
                    => _ = services.AddTransient<ConfigurationSampleService>()
                                   .AddTransient<EnvironmentSampleService>())
                 .Build();
