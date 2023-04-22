// See https://aka.ms/new-console-template for more information


int a = 1;
ChangeAValue(ref a);
Console.WriteLine($"the value of a changed to {a}");
Console.WriteLine();

SomeValue myData = new(1, 2, 3, 4);
PassValueByReferenceReadonly(in myData);
static void ChangeAValue(ref int x) => x = 2;

SomeValue one = new(1, 2, 3, 4);
SomeValue two = new(5, 6, 7, 8);
SomeValue bigger1 = Max(ref one, ref two);
ref SomeValue bigger2 = ref Max(ref one, ref two);
ref readonly SomeValue bigger3 = ref Max(ref one, ref two);
ref readonly SomeValue bigger4 = ref MaxReadOnly(in one, in two);
SomeValue bigger5 = MaxReadOnly(in one, in two);
Console.WriteLine($"The larger of {one.Sum()} and {two.Sum()} using Max() is {bigger1.Sum()}");
Console.WriteLine($"The larger of {one.Sum()} and {two.Sum()} using ref Max() is {bigger2.Sum()}");
Console.WriteLine($"The larger of {one.Sum()} and {two.Sum()} ref readonly Max() is {bigger3.Sum()}");
Console.WriteLine($"The larger of {one.Sum()} and {two.Sum()} using ref readonly MaxReadonly() is {bigger4.Sum()}");
Console.WriteLine($"The larger of {one.Sum()} and {two.Sum()} using MaxReadonly is {bigger5.Sum()}");
Console.WriteLine();

Console.Write("Please enter a number: ");
string? input = Console.ReadLine();
if (int.TryParse(input, out int x))
{
    Console.WriteLine($"read an int: {x}");
}
Console.WriteLine();

void PassValueByReferenceReadonly(in SomeValue data)
{
    // data.Value1 = 4; - you cannot change a value, it's a read-only variable!
}

ref SomeValue Max(ref SomeValue x, ref SomeValue y)
{
    int sumX = x.Sum();
    int sumY = y.Sum();

    ref SomeValue result = ref (sumX > sumY) ? ref x : ref y;
    return ref result;
}

ref readonly SomeValue MaxReadOnly(in SomeValue x, in SomeValue y)
{
    int sumX = x.Sum();
    int sumY = y.Sum();

    return ref (sumX > sumY) ? ref x : ref y;
}

#pragma warning disable CA1050 // Declare types in namespaces
public struct SomeValue
#pragma warning restore CA1050 // Declare types in namespaces
{
    public SomeValue(int value1, int value2, int value3, int value4)
    {
        Value1 = value1;
        Value2 = value2;
        Value3 = value3;
        Value4 = value4;
    }

    public readonly int Sum() => Value1 + Value2 + Value3 + Value4;

    public int Value1 { get; set; }
    public int Value2 { get; set; }
    public int Value3 { get; set; }
    public int Value4 { get; set; }
}
