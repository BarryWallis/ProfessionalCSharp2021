// See https://aka.ms/new-console-template for more information

using OperatorOverloadingSample;

Vector vector1 = new(3.0, 3.0, 1.0);
Vector vector2 = new(2.0, -4.0, -4.0);
Vector vector3 = vector1 + vector2;
Console.WriteLine($"vector1 = {vector1}");
Console.WriteLine($"vector2 = {vector2}");
Console.WriteLine($"vector3 = {vector3}");
Console.WriteLine();

vector3 += vector2;
Console.WriteLine($"vector3 = {vector3}");
Console.WriteLine();

Console.WriteLine($"2 * vector3 = {2 * vector3}");
Console.WriteLine($"vector3 += vector2 gives {vector3 += vector2}");
Console.WriteLine($"vector3 = vector1 * 2 gives {vector3 = vector1 * 2}");
Console.WriteLine($"vector1 * vector3 = {vector1 * vector3}");
