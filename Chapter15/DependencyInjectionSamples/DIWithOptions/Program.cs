// See https://aka.ms/new-console-template for more information

using DIWithOptions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder().ConfigureServices(services 
    => _ = services.AddGreetingService(options => options.From = "Christian")
                   .AddSingleton<IGreetingService, GreetingService>()
                   .AddTransient<HomeController>()).Build();

HomeController controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Katharina");
Console.WriteLine(result);
