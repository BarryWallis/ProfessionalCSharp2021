// See https://aka.ms/new-console-template for more information

AStruct x1 = new() { _a = 1 };
AStruct x2 = x1;
x2._a = 2;
Console.WriteLine($"original didn't change with a struct: {x1._a}");

AClass y1 = new() { _a = 1 };
AClass y2 = y1;
y2._a = 2;
Console.WriteLine($"original changed with a class: {y1._a}");

ARecord z1 = new() { _a = 1 };
ARecord z2 = z1;
z2._a = 2;
Console.WriteLine($"original changed with a record: {z1._a}");

(int Number, string String) t1 = (Number: 1, String: "a");
(int Number, string String) t2 = t1;
t2.Number = 2;
t2.String = "b";
Console.WriteLine($"original didn't change with a tuple: {t1.Number} {t1.String}");

#pragma warning disable CA1050 // Declare types in namespaces
public struct AStruct
#pragma warning restore CA1050 // Declare types in namespaces
{
    public int _a;
}

#pragma warning disable CA1050 // Declare types in namespaces
public class AClass
#pragma warning restore CA1050 // Declare types in namespaces
{
    public int _a;
}

#pragma warning disable CA1050 // Declare types in namespaces
public record ARecord
#pragma warning restore CA1050 // Declare types in namespaces
{
    public int _a;
}
