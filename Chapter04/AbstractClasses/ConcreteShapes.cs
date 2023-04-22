namespace AbstractClasses;
public class Rectangle : Shape
{
    public override Rectangle Clone()
    {
        Rectangle r = new();
        r.Position.X = Position.X;
        r.Position.Y = Position.Y;
        r.Size.Width = Size.Width;
        r.Size.Height = Size.Height;
        return r;
    }

    public override void Move(Position newPosition)
    {
        Console.Write($"Rectangle ");
        base.Move(newPosition);
    }

    protected override void DisplayShape()
        => Console.WriteLine($"Rectangle at position {Position} with size {Size}");
}

public class Ellipse : Shape
{
    public override Ellipse Clone()
    {
        Ellipse e = new();
        e.Position.X = Position.X;
        e.Position.Y = Position.Y;
        e.Size.Width = Size.Width;
        e.Size.Height = Size.Height;
        return e;
    }

    protected override void DisplayShape()
        => Console.WriteLine($"Ellipse at position {Position} with size {Size}");
}

