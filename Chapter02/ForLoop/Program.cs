// See https://aka.ms/new-console-template for more information

for (int i = 0; i < 100; i += 10)
{
    for (int j = i; j < i + 10; j++)
    {
        Console.Write($" {j}");
    }
    Console.WriteLine();
}
