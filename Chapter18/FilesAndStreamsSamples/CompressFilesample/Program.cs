using System.IO.Compression;
using System.Text;

internal class Program
{
#pragma warning disable IDE0210 // Convert to top-level statements
    private static void Main()
#pragma warning restore IDE0210 // Convert to top-level statements
    {
        CompressFile();
        DecompressFile();

        CompressFileWithBrotli();
        DecompressFileWithBrotli();

        CreateZipFile();
    }

    private static void CreateZipFile()
    {
        Console.WriteLine(nameof(CreateZipFile));

        string sourceDirectory;
        if (!string.IsNullOrWhiteSpace(sourceDirectory = GetFile("Source directory")))
        {
            string zipFile;
            if (!string.IsNullOrWhiteSpace(zipFile = GetFile("Zip filename")))
            {
                CreateZipFile(sourceDirectory, zipFile);
            }
        }

        Console.WriteLine();
    }

    private static void CreateZipFile(string sourceDirectory, string zipFile)
    {
        FileStream zipStream = File.Create(zipFile);
        using ZipArchive archive = new(zipStream, ZipArchiveMode.Create);
        IEnumerable<string> files = Directory.EnumerateFiles(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            if (zipStream.Name != file)
            {
                ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(file));
                using FileStream inputStream = File.OpenRead(file);
                using Stream outputStream = entry.Open();
                inputStream.CopyTo(outputStream);
            }
        }
    }

    private static void DecompressFile()
    {
        Console.WriteLine(nameof(DecompressFile));

        string filename;
        if (!string.IsNullOrWhiteSpace(filename = GetFile()))
        {
            DecompressFile(filename);
        }
        Console.WriteLine();
    }


    private static void DecompressFileWithBrotli()
    {
        Console.WriteLine(nameof(DecompressFileWithBrotli));

        string filename;
        if (!string.IsNullOrWhiteSpace(filename = GetFile()))
        {
            DecompressFileWithBrotli(filename);
        }
        Console.WriteLine();
    }
    private static void DecompressFileWithBrotli(string filename)
    {
        FileStream inputStream = File.OpenRead(filename);
        using MemoryStream outputStream = new();
        using BrotliStream brotliStream = new(inputStream, CompressionMode.Decompress);
        brotliStream.CopyTo(outputStream);
        _ = outputStream.Seek(0, SeekOrigin.Begin);
        using StreamReader reader = new(outputStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true,
                                        bufferSize: 4096, leaveOpen: true);
        string result = reader.ReadToEnd();
        Console.WriteLine(result);
    }

    private static void CompressFileWithBrotli()
    {
        Console.WriteLine(nameof(CompressFileWithBrotli));

        Console.Write($"Input file: ");
        string? inputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputFile))
        {
            return;
        }

        Console.Write($"Output file: ");
        string? outputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputFile))
        {
            return;
        }

        CompressFileWithBrotli(inputFile, outputFile);
        Console.WriteLine("Compressed");
        Console.WriteLine();
    }

    private static void CompressFileWithBrotli(string inputFile, string outputFile)
    {
        using FileStream inputStream = File.OpenRead(inputFile);
        FileStream outputStream = File.OpenWrite(outputFile);
        using BrotliStream brotliStream = new(outputStream, CompressionMode.Compress);
        inputStream.CopyTo(brotliStream);
    }

    private static void DecompressFile(string filename)
    {
        FileStream inputStream = File.OpenRead(filename);
        using MemoryStream outputStream = new();
        using DeflateStream deflateStream = new(inputStream, CompressionMode.Decompress);
        deflateStream.CopyTo(outputStream);
        _ = outputStream.Seek(0, SeekOrigin.Begin);
        using StreamReader reader = new(outputStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true,
                                        bufferSize: 4096, leaveOpen: true);
        string result = reader.ReadToEnd();
        Console.WriteLine(result);
    }

    private static void CompressFile()
    {
        Console.WriteLine(nameof(CompressFile));

        Console.Write($"Input file: ");
        string? inputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputFile))
        {
            return;
        }

        Console.Write($"Output file: ");
        string outputFile = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(outputFile))
        {
            return;
        }

        CompressFile(inputFile, outputFile);
        Console.WriteLine("Compressed");
        Console.WriteLine();
    }

    private static void CompressFile(string inputFile, string outputFile)
    {
        using FileStream inputStream = File.OpenRead(inputFile);
        FileStream outputStream = File.OpenWrite(outputFile);
        using DeflateStream deflateStream = new(outputStream, CompressionMode.Compress);
        inputStream.CopyTo(deflateStream);
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
    }

    private static string GetFile(string prompt = "Filename")
    {
        Console.Write($"{prompt}: ");
        return Console.ReadLine() ?? string.Empty;
    }

}
