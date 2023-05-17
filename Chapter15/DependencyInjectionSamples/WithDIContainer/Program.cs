// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;

using WithDIContainer;

using ServiceProvider container = GetServiceProvider();
HomeController controller = container.GetRequiredService<HomeController>();
string result = controller.Hello("Stephanie");
Console.WriteLine(result);

static ServiceProvider GetServiceProvider()
{
    ServiceCollection services = new();
    _ = services.AddSingleton<IGreetingService, GreetingService>()
                .AddTransient<HomeController>();
    return services.BuildServiceProvider();
}
