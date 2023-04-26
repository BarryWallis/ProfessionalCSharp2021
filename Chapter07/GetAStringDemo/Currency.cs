namespace GetAStringDemo;
public struct Currency
{
#pragma warning disable IDE1006 // Naming Styles
    public uint Dollars;
    public ushort Cents;
#pragma warning restore IDE1006 // Naming Styles

    public Currency(uint dollars, ushort cents)
    {
        Dollars = dollars;
        Cents = cents;
    }

    public override readonly string? ToString() => $"${Dollars}.{Cents,2:00}";

    public static string GetCurrencyUnit() => "Dollar";

    public static explicit operator Currency(float value)
    {
        checked
        {
            uint dollars = (uint)value;
            ushort cents = (ushort)((value - dollars) * 100);
            return new(dollars, cents);
        }
    }

    public static implicit operator float(Currency currency) => currency.Dollars + (currency.Cents / 100f);

    public static implicit operator Currency(uint value) => new(value, 0);

    public static implicit operator uint(Currency currency) => currency.Dollars;
}
