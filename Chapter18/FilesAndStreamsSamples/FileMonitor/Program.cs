// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("File Monitor");

FileSystemWatcher? watcher;
string? directoryName;
do
{
    directoryName = GetFile("Directory name");
} while (string.IsNullOrEmpty(directoryName));

WatchFiles(directoryName, "*.md");
Console.WriteLine("Press [Enter] to stop watching");
Console.ReadLine();
UnwatchFiles();

void UnwatchFiles()
{
    Debug.Assert(watcher is not null);
    watcher.EnableRaisingEvents = false;
}

void WatchFiles(string directoryName, string filter)
{
    watcher = new(directoryName, filter) { IncludeSubdirectories = true };
    watcher.Created += OnFileChanged;
    watcher.Changed += OnFileChanged;
    watcher.Deleted += OnFileChanged;
    watcher.Renamed += OnFileChanged;
    watcher.EnableRaisingEvents = true;
    Console.WriteLine("watching file changes...");
}

void OnFileChanged(object sender, FileSystemEventArgs e) => Console.WriteLine($"file {e.Name} {e.ChangeType}");

static string GetFile(string prompt = "Filename")
{
    Console.Write($"{prompt}: ");
    return Console.ReadLine() ?? string.Empty;
}
