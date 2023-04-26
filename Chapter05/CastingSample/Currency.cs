namespace CastingSample;
public readonly struct Currency
{
#pragma warning disable IDE1006 // Naming Styles
    public readonly uint Dollars;
    public readonly ushort Cents;
#pragma warning restore IDE1006 // Naming Styles

    public Currency(uint dollars, ushort cents)
    {
        Dollars = dollars;
        Cents = cents;
    }

    public override string? ToString() => $"${Dollars}.{Cents,-2:00}";

    public static implicit operator float(Currency currency) => currency.Dollars + (currency.Cents / 100.0f);

    public static explicit operator Currency(float value)
    {
        checked
        {
            uint dollars = (uint)value;
            ushort cents = Convert.ToUInt16((value - dollars) * 100);
            return new(dollars, cents);
        }
    }
}
