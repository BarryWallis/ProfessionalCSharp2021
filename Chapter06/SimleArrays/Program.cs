// See https://aka.ms/new-console-template for more information

int[][] jagged =
{
    new[] {1, 2 },
    new[] { 3, 4, 5, 6, 7, 8 },
    new[] {9, 10, 11}
};

for (int row = 0; row < jagged.Length; row++)
{
    for (int element = 0; element < jagged[row].Length; element++)
    {
        Console.WriteLine($"row: {row}, element: {element}, value: {jagged[row][element]}");
    }
}
Console.WriteLine();

Array intArray1 = Array.CreateInstance(typeof(int), 5);
for (int i = 0; i < 5; i++)
{
    intArray1.SetValue(3 * i, i);
}

for (int i = 0; i < 5; i++)
{
    Console.WriteLine(intArray1.GetValue(i));
}
