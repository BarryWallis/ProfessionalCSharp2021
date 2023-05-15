internal class Program
{
#pragma warning disable IDE0210 // Convert to top-level statements
    private static unsafe void Main() // Top-level statements don't support "unsafe"
#pragma warning restore IDE0210 // Convert to top-level statements
    {
        int a = 10;
        short b = -1;
        byte c = 4;
        float d = 1.5F;
        int* pa = &a;
        short* pb = &b;
        byte* pc = &c;
        float* pd = &d;

        Console.WriteLine($"Address of a is 0x{(ulong)&a:X}, size is {sizeof(int)}, value is {a}");
        Console.WriteLine($"Address of b is 0x{(ulong)&b:X}, size is {sizeof(short)}, value is {b}");
        Console.WriteLine($"Address of c is 0x{(ulong)&c:X}, size is {sizeof(byte)}, value is {c}");
        Console.WriteLine($"Address of d is 0x{(ulong)&d:X}, size is {sizeof(float)}, value is {d}");
        Console.WriteLine($"Address of pa = &a is 0x{(ulong)&pa:X}, size is {sizeof(int*)}, " +
            $"value is {(ulong)pa:X}");
        Console.WriteLine($"Address of pb = &b is 0x{(ulong)&pb:X}, size is {sizeof(short*)}, " +
            $"value is {(ulong)pb:X}");
        Console.WriteLine($"Address of pc = &c is 0x{(ulong)&pc:X}, size is {sizeof(byte*)}, " +
            $"value is {(ulong)pc:X}");
        Console.WriteLine($"Address of pd = &d is 0x{(ulong)&pd:X}, size is {sizeof(float*)}, " +
            $"value is {(ulong)pd:X}");

        *pa = 20;
        Console.WriteLine($"After setting *pa, a = {a}");
        Console.WriteLine($"*pa = {*pa}");

        pd = (float*)pa;
        Console.WriteLine($"a treated as float = {*pd}");
    }
}
