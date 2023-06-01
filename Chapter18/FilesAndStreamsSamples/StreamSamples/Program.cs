using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Schema;

internal class Program
{
#pragma warning disable IDE0210 // Convert to top-level statements
    private static async Task Main(string[] args)
#pragma warning restore IDE0210 // Convert to top-level statements
    {
        UseStreamOn(ReadFileUsingFileStream, nameof(ReadFileUsingFileStream));
        WriteTextFile();
        CopyUsingStreams();
        await CreateSampleFileAsync();
        await RandomAccessSampleAsync();
    }

    private static async Task RandomAccessSampleAsync()
    {
        const int recordSize = 44;
        try
        {
            using FileStream stream = File.OpenRead(_sampleDataFilePath);
            Memory<byte> buffer = new byte[recordSize].AsMemory();

            do
            {
                try
                {
                    Console.Write("record number (or 'bye' to end): ");
                    string line = Console.ReadLine() ?? throw new InvalidOperationException();
                    if (string.Equals(line, "bye", StringComparison.CurrentCultureIgnoreCase))
                    {
                        break;
                    }

                    if (int.TryParse(line, out int recordNumber))
                    {
                        _ = stream.Seek((recordNumber - 1) * recordSize, SeekOrigin.Begin);
                        int nRead = await stream.ReadAsync(buffer);
                        string record = Encoding.UTF8.GetString(buffer.Span[0..nRead]);
                        Console.WriteLine($"record: {record}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
            Console.WriteLine("finished");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Create the sample file");
        }

    }

    private static async Task CreateSampleFileAsync()
    {
        Console.WriteLine(nameof(CreateSampleFileAsync));

        int numberOfRecords;
        bool success;
        do
        {
            Console.Write("Number of records to create: ");
            string? input = Console.ReadLine();
            success = int.TryParse(input, out numberOfRecords);
        } while (!success || numberOfRecords <= 0);

        await CreateSampleFileAsync(numberOfRecords);
        Console.WriteLine();
    }

    private static readonly string _sampleDataFilePath
        = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "samplefile.data");

    private static async Task CreateSampleFileAsync(int numberOfRecords)
    {
        FileStream stream = File.Create(_sampleDataFilePath);
        using StreamWriter writer = new(stream);
        Random random = new();
        IEnumerable<(int Number, string Text, DateTime Date)> records
            = Enumerable.Range(1, numberOfRecords).Select(n =>
            (
                Number: n,
                Text: $"Sample text {random.Next(200)}",
                Date: new DateTime(Math.Abs((long)(((random.NextDouble() * 2) - 1) * DateTime.MaxValue.Ticks)))
            ));

        Console.WriteLine("Start writing records...");
        foreach ((int Number, string Text, DateTime Date) in records)
        {
            string date = Date.ToString("d", CultureInfo.InvariantCulture);
            string s = $"#{Number,8};{Text,-20};{date}#{Environment.NewLine}";
            await writer.WriteAsync(s);
        }

        Console.WriteLine($"Created the file {_sampleDataFilePath}");
    }

    private static void CopyUsingStreams()
    {
        Console.WriteLine(nameof(CopyUsingStreams));

        string? inputFile;
        do
        {
            Console.Write("Input file: ");
            inputFile = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(inputFile));

        string? outputFile;
        do
        {
            Console.Write("Output file: ");
            outputFile = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(outputFile));

        CopyUsingStreams(inputFile, outputFile);
        Console.WriteLine();
    }

    private static void CopyUsingStreams(string inputFile, string outputFile)
    {
        const int bufferSize = 4096;
        using FileStream inputStream = File.OpenRead(inputFile);
        using FileStream outputStream = File.OpenWrite(outputFile);
        Span<byte> buffer = new byte[bufferSize].AsSpan();
        int nRead;
        do
        {
            nRead = inputStream.Read(buffer);
            if (nRead > 0)
            {
                outputStream.Write(buffer[..nRead]);
            }
        } while (nRead > 0);
    }

    private static void WriteTextFile()
    {
        Console.WriteLine(nameof(WriteTextFile));

        string tempFilename = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        string tempTextFilename = Path.ChangeExtension(tempFilename, "txt");
        using FileStream stream = File.OpenWrite(tempTextFilename);
        Span<byte> preamble = Encoding.UTF8.GetPreamble().AsSpan();
        stream.Write(preamble);
        string hello = "Hello, World";
        Span<byte> buffer = Encoding.UTF8.GetBytes(hello).AsSpan();
        stream.Write(buffer);
        Console.WriteLine($"file {stream.Name} written");
        Console.WriteLine();
    }

    private static void GetEncoding(string filename)
    {
        using FileStream fileStream = new(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        _ = GetEncoding(fileStream);
    }

    private static Encoding GetEncoding(Stream stream)
    {
        if (!stream.CanSeek)
        {
            throw new ArgumentException("require a stream that can seek");
        }

        byte[] bom = new byte[5];
        int nRead = stream.Read(bom, offset: 0, count: 5);
        if (nRead >= bom.Length && bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0x00 && bom[3] == 0x00)
        {
            Console.WriteLine($"UTF-32");
            _ = stream.Seek(4, SeekOrigin.Begin);
            return Encoding.UTF32;
        }
        else if (nRead >= 3 && bom[0] == 0xff && bom[1] == 0xfe)
        {
            Console.WriteLine("UTF-16 / Unicode, little endian");
            _ = stream.Seek(2, SeekOrigin.Begin);
            return Encoding.Unicode;
        }
        else if (nRead >= 3 && bom[0] == 0xfe && bom[1] == 0xff)
        {
            Console.WriteLine("UTF-16 / Unicode, big endian");
            _ = stream.Seek(2, SeekOrigin.Begin);
            return Encoding.BigEndianUnicode;
        }
        else if (nRead >= 4 && bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
        {
            Console.WriteLine($"UTF-8");
            _ = stream.Seek(3, SeekOrigin.Begin);
            return Encoding.UTF8;
        }
        else
        {
            Console.WriteLine($"ASCII");
            _ = stream.Seek(0, SeekOrigin.Begin);
            return Encoding.ASCII;
        }
    }

    private static void UseStreamOn(Action<string> action, string title = "")
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

    private static void ReadFileUsingFileStream(string filename)
    {
        const int bufferSize = 4096;
        using FileStream stream = new(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        ShowStreamInformation(stream);
        Encoding encoding = GetEncoding(stream);

        Span<byte> buffer = new byte[bufferSize].AsSpan();
        bool completed = false;
        do
        {
            int nRead = stream.Read(buffer);
            if (nRead == 0)
            {
                completed = true;
            }
            if (nRead < buffer.Length)
            {
                buffer[nRead..].Clear();
            }

            string s = encoding.GetString(buffer[..nRead]);
            Console.WriteLine($"read {nRead} bytes");
            Console.WriteLine(s);
        } while (!completed);

        Console.WriteLine();
    }

    private static void ShowStreamInformation(FileStream stream)
    {
        Console.WriteLine($"stream can read: {stream.CanRead}, can write: {stream.CanWrite}, " +
            $"can seek: {stream.CanSeek}, can timeout: {stream.CanTimeout}");
        Console.WriteLine($"length: {stream.Length}, position: {stream.Position}");
        if (stream.CanTimeout)
        {
            Console.WriteLine($"read timeout: {stream.ReadTimeout}, write timeout: {stream.WriteTimeout}");
        }
    }
}
