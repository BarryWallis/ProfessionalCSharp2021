// See https://aka.ms/new-console-template for more information

using MulticastDelegates;

Action<double> operations = MathOperations.MultiplyByTwo;
operations += MathOperations.Square;
ProcessAndDisplayNumber(operations, 2.0);
ProcessAndDisplayNumber(operations, 7.94);
ProcessAndDisplayNumber(operations, 1.414);

static void ProcessAndDisplayNumber(Action<double> action, double value)
{
    Console.WriteLine($"{nameof(ProcessAndDisplayNumber)} called with value = {value}");
    action(value);
    Console.WriteLine();
}
