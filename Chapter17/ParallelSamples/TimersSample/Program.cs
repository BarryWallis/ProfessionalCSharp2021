// See https://aka.ms/new-console-template for more information

static void TimeAction(object? o) => Console.WriteLine($"System.Threading.Timer. {DateTime.Now:T}");

using Timer timer1 = new(TimeAction, null, dueTime: TimeSpan.FromSeconds(2), period: TimeSpan.FromSeconds(3));

await Task.Delay(15000);
