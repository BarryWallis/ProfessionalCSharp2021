using InheritanceWithConstructors;
// See https://aka.ms/new-console-template for more information

Rectangle r1 = new(33, 22, 200, 100);
Rectangle r2 = r1.Clone();
r2.Position.X = 300;

Ellipse e1 = new(122, 200, 40, 20);

DisplayShapes(r1, r2, e1);

r1.Move(new Position(120, 40));

static void DisplayShapes(params Shape[] shapes)
{
    foreach (Shape shape in shapes)
    {
        shape.Draw();
    }
}
