namespace UsingInterfaces;
public abstract class Shape
{
    public Shape(ILogger logger) => Logger = logger;

    protected ILogger Logger { get; }
    public Position? Position { get; init; }
    public Size? Size { get; init; }

    public void Draw() => DisplayShape();

    protected virtual void DisplayShape() => Logger.Log($"Shape with {Position} and {Size}");
}

public record Position(int X, int Y)
{
    public override string? ToString() => $"X: {X}, Y: {Y}";
}

public record Size(int Width, int Height)
{
    public override string? ToString() => $"Width: {Width}, Height: {Height}";
}
