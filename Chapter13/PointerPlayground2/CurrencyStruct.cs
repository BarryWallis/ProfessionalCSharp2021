using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointerPlayground2;
internal readonly struct CurrencyStruct
{
#pragma warning disable IDE1006 // Naming Styles
    public readonly long Dollars;
    public readonly byte Cents;
#pragma warning restore IDE1006 // Naming Styles

    public CurrencyStruct(long dollars, byte cents)
    {
        Dollars = dollars;
        Cents = cents;
    }

    public override string ToString() => $"${Dollars}.{Cents}";
}

internal class CurrencyClass
{
#pragma warning disable IDE1006 // Naming Styles
    public readonly long Dollars = 0;
    public readonly byte Cents = 0;
#pragma warning restore IDE1006 // Naming Styles

    public CurrencyClass(long dollars, byte cents)
    {
        Dollars = dollars;
        Cents = cents;
    }

    public override string ToString() => $"${Dollars}.{Cents}";
}
