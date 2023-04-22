namespace InheritanceWithConstructors;
public abstract class Shape
{
    protected Shape(int x, int y, int width, int height)
    {
        Position = new(x, y);
        Size = new(width, height);
    }

    public virtual Position Position { get; }
    public virtual Size Size { get; }

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
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public override string? ToString() => $"X: {X}, Y: {Y}";
}

public class Size
{
    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get; set; }
    public int Height { get; set; }

    public override string? ToString() => $"Width: {Width}, Height: {Height}";
}
