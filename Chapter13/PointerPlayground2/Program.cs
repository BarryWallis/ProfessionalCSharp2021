using System.Reflection.Metadata;
using System.Security.Authentication;

using PointerPlayground2;

internal class Program
{
#pragma warning disable IDE0210 // Convert to top-level statements
    private static unsafe void Main()
#pragma warning restore IDE0210 // Convert to top-level statements
    {
        Console.WriteLine($"Size of CurrencyStruct is {sizeof(CurrencyStruct)}");
        CurrencyStruct amount1 = new(10, 10);
        CurrencyStruct amount2 = new(20, 20);
        CurrencyStruct* pAmount = &amount1;
        long* pDollars = &pAmount->Dollars;
        byte* pCents = &pAmount->Cents;

        Console.WriteLine($"Address of amount1 is 0x{(ulong)&amount1:X}");
        Console.WriteLine($"Address of amount2 is 0x{(ulong)&amount2:X}");
        Console.WriteLine($"Address of pAmount is 0x{(ulong)&pAmount:X}");
        Console.WriteLine($"The value of pAmount is 0x{(ulong)pAmount:X}");
        Console.WriteLine($"Address of pDollars is 0x{(ulong)&pDollars:X}");
        Console.WriteLine($"The value of pDollars is 0x{(ulong)pDollars:X}");
        Console.WriteLine($"Address of pCents is 0x{(ulong)&pCents:X}");
        Console.WriteLine($"The value of pCents is 0x{(ulong)pCents:X}");

        *pDollars = 100; // Note: Even though Dollars is readonly, you can still change it via a pointer.
        Console.WriteLine($"amount1 contains {amount1}");

        pAmount -= 1; // This should point to amount2 since the stack grows downward.
        Console.WriteLine($"amount2 has address 0x{(ulong)pAmount:X} and contains {*pAmount}");

        CurrencyStruct* pTempCurrency = (CurrencyStruct*)pCents;
        pCents = (byte*)(--pTempCurrency);
        Console.WriteLine($"Value of pCents is now 0x{(ulong)pCents:X}");
        Console.WriteLine($"The value where pCents points to: {*pCents}");
        Console.WriteLine();

        Console.WriteLine($"Now with classes");
        CurrencyClass amount3 = new(30, 0);
        fixed (long* pDollars2 = &amount3.Dollars)
        fixed(byte* pCents2 = &amount3.Cents)
        {
            Console.WriteLine($"amount3.Dollars has address 0x{(ulong)pDollars2:X}");
            Console.WriteLine($"amount3.Cents has address 0x{(ulong)pCents2:X}");
            *pDollars2 = -100;
            Console.WriteLine($"amount3 contains {amount3}");
        }
        Console.WriteLine();

        static int Add(int x, int y) => x + y;
        FunctionPointerSample.Calc(&Add);
    }
}
