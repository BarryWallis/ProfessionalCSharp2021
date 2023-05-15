internal class Program
{
#pragma warning disable IDE0210 // Convert to top-level statements
    private static unsafe void Main()
#pragma warning restore IDE0210 // Convert to top-level statements
    {
        string? userInput;
        int size;
        do
        {
            Console.Write($"How big an array do you want? ");
            userInput = Console.ReadLine();
        } while (!int.TryParse(userInput, out size));

        long* pArray = stackalloc long[size];
        for (int i = 0; i < size; i++)
        {
            pArray[i] = i * i;
        }

        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"Element {i} = {*(pArray + i)}");
        }
    }
}
