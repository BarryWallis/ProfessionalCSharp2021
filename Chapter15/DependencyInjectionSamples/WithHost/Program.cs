// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WithHost;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services
    => _ = services.AddSingleton<IGreetingService, GreetingService>()
                   .AddTransient<HomeController>()).Build();

HomeController controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Matthias");
Console.WriteLine(result);
