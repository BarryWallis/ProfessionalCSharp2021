// See https://aka.ms/new-console-template for more information

using DIWithConfiguration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder().ConfigureServices((context,
                                                                  services) =>
{
    IConfiguration configuration = context.Configuration;
    _ = services.AddGreetingService(configuration.GetSection("GreetingService"))
                .AddSingleton<IGreetingService, GreetingService>()
                .AddTransient<HomeController>();
}).Build();

HomeController controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Katharina");
Console.WriteLine(result);
