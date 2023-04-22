namespace InheritanceWithConstructors;
public class Rectangle : Shape
{
    public Rectangle(int x, int y, int width, int height) : base(x, y, width, height)
    {
    }

    public override Rectangle Clone()
    {
        Rectangle r = new(Position.X, Position.Y, Size.Width, Size.Height);
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
    public Ellipse(int x, int y, int width, int height) : base(x, y, width, height)
    {
    }

    public override Ellipse Clone()
    {
        Ellipse e = new(Position.X, Position.Y, Size.Width, Size.Height);
        e.Position.X = Position.X;
        e.Position.Y = Position.Y;
        e.Size.Width = Size.Width;
        e.Size.Height = Size.Height;
        return e;
    }

    protected override void DisplayShape()
        => Console.WriteLine($"Ellipse at position {Position} with size {Size}");
}

