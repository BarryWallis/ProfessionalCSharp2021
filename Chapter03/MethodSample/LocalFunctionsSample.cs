namespace MethodSample;
public class LocalFunctionsSample
{
    public static void IntroLocalFunctions()
    {
        static int Add(int x, int y) => x + y;

        int result = Add(3, 7);
        Console.WriteLine($"called the local function with this result: {result}");
    }

    public static void LocalFunctionWithClosure()
    {
        int z = 3;

        int result = Add(1, 2);
        Console.WriteLine($"called the local function with this result: {result}");

        int Add(int x, int y) => x + y + z;
    }
}
