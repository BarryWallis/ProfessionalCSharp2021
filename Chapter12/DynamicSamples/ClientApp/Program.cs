// See https://aka.ms/new-console-template for more information

using System.Reflection;

using Microsoft.CSharp.RuntimeBinder;

const string CalculatorTypeName = "CalculatorLib.Calculator";

if (args.Length != 1)
{
    ShowUsage();
    return;
}
UsingReflection(args[0]);
UsingReflectionWithDynamic(args[0]);

object? GetCalculator(string pathToCalculator)
{
    Assembly assembly = Assembly.LoadFile(pathToCalculator);
    return assembly.CreateInstance(CalculatorTypeName);
}

void UsingReflectionWithDynamic(string pathToCalculator)
{
    double x = 3;
    double y = 4;
    dynamic calculator = GetCalculator(pathToCalculator)
                         ?? throw new InvalidOperationException($"{nameof(GetCalculator)} returned null");
    double result = calculator.Add(x, y);
    Console.WriteLine($"the result of {x} and {y} is {result}");

    try
    {
        result = calculator.Multiply(x, y);
    }
    catch (RuntimeBinderException ex)
    {
        Console.WriteLine(ex);
    }
}

void UsingReflection(string pathToCalculator)
{
    double x = 3;
    double y = 4;
    object calculator = GetCalculator(pathToCalculator) 
                        ?? throw new InvalidOperationException($"{nameof(GetCalculator)} returned null");
    object? result = calculator.GetType().GetMethod("Add")?.Invoke(calculator, new object[] { x, y })
                     ?? throw new InvalidOperationException($"Add() method not found");
    Console.WriteLine($"the result of {x} and {y} is {result}");
}

void ShowUsage()
{
    Console.WriteLine($"Usage: ClientApp path");
    Console.WriteLine();
    Console.WriteLine($"Copy CalculatorLib to a known directory and pass the absolute path of that directory.");
    Console.WriteLine($"when starting the application to load the library");
}
