// See https://aka.ms/new-console-template for more information

using GetAStringDemo;

int x = 40;
GetAString firstStringMethod = new(x.ToString);
Console.WriteLine($"String is {firstStringMethod()}"); ;

Currency balance = new(34, 50);

firstStringMethod = balance.ToString;
Console.WriteLine($"String is {firstStringMethod()}");

firstStringMethod = new GetAString(Currency.GetCurrencyUnit);
Console.WriteLine($"String is {firstStringMethod()}");
Console.WriteLine();

DoubleOp[] operations =
{
    MathOperations.MultiplyByTwo,
    MathOperations.Square,
};

for (int i = 0; i < operations.Length; i++)
{
    Console.WriteLine($"Using operations [{i}]");
    ProcessAndDisplayNumber(operations[i], 2.0);
    ProcessAndDisplayNumber(operations[i], 7.94);
    ProcessAndDisplayNumber(operations[i], 1.414);
    Console.WriteLine();
}

static void ProcessAndDisplayNumber(DoubleOp doubleOp, double value)
{
    double result = doubleOp(value);
    Console.WriteLine($"Value is {value}, result of operation is {result}");
}

#pragma warning disable CA1050 // Declare types in namespaces
public delegate string? GetAString();
public delegate double DoubleOp(double value);
#pragma warning restore CA1050 // Declare types in namespaces
