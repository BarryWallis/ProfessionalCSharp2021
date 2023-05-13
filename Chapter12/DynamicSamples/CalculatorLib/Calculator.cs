namespace CalculatorLib;

public class Calculator
{
    public Calculator()
    {
    }

#pragma warning disable CA1822 // Mark members as static
    public double Add(double x, double y) => x + y;
    public double Subtract(double x, double y) => x - y;
#pragma warning restore CA1822 // Mark members as static
}
