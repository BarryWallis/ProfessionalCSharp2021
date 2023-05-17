// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Hosting;

namespace ConfigurationSample;

public class EnvironmentSampleService
{
    private readonly IHostEnvironment _hostEnvironment;

    public EnvironmentSampleService(IHostEnvironment hostEnvironment) => _hostEnvironment = hostEnvironment;

    public void ShowHostEnvironment()
    {
        Console.WriteLine(_hostEnvironment.EnvironmentName);
        if (_hostEnvironment.IsDevelopment())
        {
            Console.WriteLine("it's a development environment");
        }
    }
}
