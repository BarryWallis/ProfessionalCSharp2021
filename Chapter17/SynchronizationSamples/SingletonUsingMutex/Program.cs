// See https://aka.ms/new-console-template for more information

_ = new Mutex(false, "SingletonAppMutex", out bool mutexCreated);
if (!mutexCreated)
{
    Console.WriteLine("You can only start one instance of the application.");
    await Task.Delay(3000);
    Console.WriteLine("Exiting.");
    return;
}

Console.WriteLine("Application running");
Console.Write("Press return to exit> ");
_ = Console.ReadLine();
