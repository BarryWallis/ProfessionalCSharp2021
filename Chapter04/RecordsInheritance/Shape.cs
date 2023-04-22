namespace RecordsInheritance;
public abstract record Shape(Position Position, Size Size)
{
    protected virtual void DisplayShape() => Console.WriteLine($"Shape with {Position} and {Size}");

    public void Draw() => DisplayShape();
}

public record Position(int X, int Y)
{
    public override string? ToString() => $"X: {X}, Y: {Y}";
}

public record Size(int Width, int Height)
{
    public override string? ToString() => $"Width: {Width}, Height: {Height}";
}
