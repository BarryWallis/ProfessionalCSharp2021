namespace AbstractClasses;
public abstract class Shape
{
    public virtual Position Position { get; } = new();
    public virtual Size Size { get; } = new();

    public abstract Shape Clone();

    public void Draw() => DisplayShape();
    public virtual void Move(Position newPosition)
    {
        Position.X = newPosition.X;
        Position.Y = newPosition.Y;
        Console.WriteLine($"moves to {Position}");
    }

    protected virtual void DisplayShape() => Console.WriteLine($"Shape with {Position} and {Size}");
}

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public override string? ToString() => $"X: {X}, Y: {Y}";
}

public class Size
{
    public int Width { get; set; }
    public int Height { get; set; }

    public override string? ToString() => $"Width: {Width}, Height: {Height}";
}
