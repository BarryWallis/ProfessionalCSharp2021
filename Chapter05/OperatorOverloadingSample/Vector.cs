namespace OperatorOverloadingSample;
public readonly struct Vector
{
    public Vector(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector(Vector vector) => (X, Y, Z) = (vector.X, vector.Y, vector.Z);

#pragma warning disable IDE1006 // Naming Styles
    public readonly double X;
    public readonly double Y;
    public readonly double Z;
#pragma warning restore IDE1006 // Naming Styles

    public static Vector operator +(Vector left, Vector right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static Vector operator *(Vector left, Vector right)
        => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    public static Vector operator *(double left, Vector right)
        => new(left * right.X, left * right.Y, left * right.Z);

    public static Vector operator *(Vector left, double right) => right * left;

    public override string? ToString() => $"({X}, {Y}, {Z})";
}

