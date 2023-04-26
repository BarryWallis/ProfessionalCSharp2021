// See https://aka.ms/new-console-template for more information

Func<string, string> oneParam = s => $"change uppercase to {s.ToUpper()}";
Console.WriteLine(oneParam("test"));

Func<double, double, double> twoParams = (x, y) => x * y;
Console.WriteLine(twoParams(3, 2));

int someValue = 5;
Func<int, int> f = x => x + someValue;
Console.WriteLine(f(3));
someValue = 7;
Console.WriteLine(f(3));
