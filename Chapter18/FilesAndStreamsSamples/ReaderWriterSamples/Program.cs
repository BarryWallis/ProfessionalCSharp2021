UseStreamOn(ReadFileUsingReader, nameof(ReadFileUsingReader));
UseStreamOn(WriteFileUsingBinaryWriter, nameof(WriteFileUsingBinaryWriter));
UseStreamOn(ReadFileUsingBinaryReader, nameof(ReadFileUsingBinaryReader));

void ReadFileUsingBinaryReader(string filename)
{
    FileStream inputStream = File.Open(filename, FileMode.Open);
    using BinaryReader reader = new(inputStream);
    double d = reader.ReadDouble();
    int i = reader.ReadInt32();
    long l = reader.ReadInt64();
    string s = reader.ReadString();
    Console.WriteLine($"d: {d}, i: {i}, l: {l}, s: {s}");
}

void WriteFileUsingBinaryWriter(string filename)
{
    FileStream outputStream = File.Create(filename);
    using BinaryWriter writer = new(outputStream);
    double d = 47.47;
    int i = 42;
    long l = 987654321;
    string s = "sample";
    writer.Write(d);
    writer.Write(i);
    writer.Write(l);
    writer.Write(s);
}

void ReadFileUsingReader(string filename)
{
    FileStream stream = new(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
    using StreamReader reader = new (stream);
    while (!reader.EndOfStream)
    {
        string? line = reader.ReadLine();
        Console.WriteLine(line);
    }
}

void UseStreamOn(Action<string> action, string title = "")
{
    Console.WriteLine(title);

    string? filename;
    while (!string.IsNullOrWhiteSpace(filename = GetFile()))
    {
        action(filename);
    }
    Console.WriteLine();

    static string? GetFile()
    {
        Console.Write($"Filename: ");
        return Console.ReadLine();
    }
}
