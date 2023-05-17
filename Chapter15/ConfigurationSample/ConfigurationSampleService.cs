using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace ConfigurationSample;
public class ConfigurationSampleService
{
    private readonly IConfiguration _configuration;

    public ConfigurationSampleService(IConfiguration configuration) => _configuration = configuration;

    public void ShowConfiguration()
    {
        string value1 = _configuration.GetValue<string>("Key1") ?? throw new InvalidDataException();
        Console.WriteLine(value1);
        string value1b = _configuration["Key1"] ?? throw new InvalidDataException(); ;
        Console.WriteLine(value1b);
        string value2 = _configuration.GetSection("Section1")["Key2"] ?? throw new InvalidDataException(); ;
        Console.WriteLine(value2);
        string connectionString = _configuration.GetConnectionString("BooksConnection")
                                  ?? throw new InvalidDataException();
        Console.WriteLine(connectionString);
        Console.WriteLine();
    }
    
    public void ShowType()
    {
        Console.WriteLine(nameof(ShowType));
        IConfigurationSection section = _configuration.GetSection("SomeTyoedConfig");
        StronglyTypedConfig? typedConfig 
            = section.Get<StronglyTypedConfig>(binder => binder.BindNonPublicProperties = true);
        Console.WriteLine(typedConfig);
        Console.WriteLine();
    }
}
