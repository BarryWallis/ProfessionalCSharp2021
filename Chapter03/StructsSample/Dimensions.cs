namespace StructsSample;
#pragma warning disable IDE0250 // Make struct 'readonly'
public struct Dimensions
#pragma warning restore IDE0250 // Make struct 'readonly'
{
    public double Length { get; }
    public double Width { get; }

    public Dimensions(double length, double width)
    {
        Length = length;
        Width = width;
    }

#pragma warning disable IDE0251 // Make member 'readonly'
    public double Diagonal => Math.Sqrt((Length * Length) + (Width * Width));
#pragma warning restore IDE0251 // Make member 'readonly'
}
