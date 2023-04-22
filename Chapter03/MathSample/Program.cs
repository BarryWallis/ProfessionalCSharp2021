// See https://aka.ms/new-console-template for more information

int x = MathSample.Math.GetSquareOf(5);
Console.WriteLine($"Square of 5 is {x}");

MathSample.Math math = new()
{
    Value = 30
};
Console.WriteLine($"Value field of math variable contains {math.Value}");
Console.WriteLine($"Square of 30 is {math.GetSquare()}");
