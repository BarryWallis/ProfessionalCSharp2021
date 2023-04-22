// See https://aka.ms/new-console-template for more information

using MethodSample;

LocalFunctionsSample.IntroLocalFunctions();
Console.WriteLine();

LocalFunctionsSample.LocalFunctionWithClosure();
Console.WriteLine();

string x = "123";
string y = "456";
Console.WriteLine($"Before swap: x = {x}, y = {y}");
GenericMethods.Swap(ref x, ref y);
Console.WriteLine($"After swap: x = {x}, y = {y}");
Console.WriteLine();
