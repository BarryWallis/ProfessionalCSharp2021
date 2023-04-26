// See https://aka.ms/new-console-template for more information

Action delegate1 = One;
delegate1 += Two;
Delegate[] delegates = delegate1.GetInvocationList();
foreach (Action d in delegates.Cast<Action>())
{
    try
    {
        d();
    }
    catch (Exception)
    {
        Console.WriteLine("Exception caught");
    }
}

static void One()
{
    Console.WriteLine("One");
    throw new Exception("Error in One");
}

static void Two() => Console.WriteLine("Two");
