// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

internal class Program
{
    private int _someProperty;
    public int SomeProperty
    {
        get => _someProperty;
        set
        {
            Log();
            _someProperty = value;
        }
    }

    private static void Main()
    {
        Program program = new();
        Log();
        program.SomeProperty = 33;
        static void action1() => Log();
        action1();
    }

    private static void Log([CallerLineNumber] int line = -1,
                            [CallerFilePath] string? path = default,
                            [CallerMemberName] string? name = default)
    {
        Console.WriteLine($"Line {line}");
        Console.WriteLine(path);
        Console.WriteLine(name);
        Console.WriteLine();
    }
}
