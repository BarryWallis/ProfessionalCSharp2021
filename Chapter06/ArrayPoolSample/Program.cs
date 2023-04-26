// See https://aka.ms/new-console-template for more information

using System.Buffers;

UseSharedPool();

static void UseSharedPool()
{
    for (int i = 0; i < 10; i++)
    {
        int arrayLength = (i + 1) << 10;
        int[] array = ArrayPool<int>.Shared.Rent(arrayLength);
        Console.WriteLine($"requested an array of {arrayLength} and received {array.Length}");
        ArrayPool<int>.Shared.Return(array, clearArray: true);
    }
    Console.WriteLine();
}
