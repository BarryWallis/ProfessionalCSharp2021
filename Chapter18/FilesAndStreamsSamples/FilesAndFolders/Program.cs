using System.Runtime.CompilerServices;

internal class Program
{
#pragma warning disable IDE0210 // Convert to top-level statements
    private static void Main()
#pragma warning restore IDE0210 // Convert to top-level statements
    {
        ShowDrives();
        ShowSpecialFolders();
        CreateFile();
        FileInformation();
        ChangeFileProperties();
        ReadLineByLine();
        WriteAFile();
        DeleteDuplicateFiles();
    }

    private static void DeleteDuplicateFiles()
    {
        Console.WriteLine(nameof(DeleteDuplicateFiles));

        string? input;
        while (!string.IsNullOrWhiteSpace(input = GetDirectory()))
        {
            DeleteDuplicateFiles(input);
        }

        Console.WriteLine();

        static string? GetDirectory()
        {
            Console.Write($"Directory: ");
            return Console.ReadLine();
        }
    }

    private static void DeleteDuplicateFiles(string directory)
    {
        IEnumerable<string> filenames = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories);
        string previousFilename = string.Empty;
        foreach (string filename in filenames)
        {
            string previousName = Path.GetFileNameWithoutExtension(previousFilename);
            const string copySuffix = " - Copy";
            int ix = previousFilename.LastIndexOf(copySuffix);
            if (!string.IsNullOrEmpty(previousFilename)
                && previousName.EndsWith(copySuffix)
                && filename.StartsWith(previousFilename[..ix]))
            {
                FileInfo copiedFile = new(previousFilename);
                FileInfo originalFile = new(filename);
                if (copiedFile.Length == originalFile.Length)
                {
                    Console.WriteLine($"delete {copiedFile.FullName}");
                    copiedFile.Delete();
                }
            }

            previousFilename = filename;
        }
    }

    private static void WriteAFile()
    {
        Console.WriteLine(nameof(WriteAFile));

        string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                       "movies.txt");
        string[] movies =
        {
            "Snow White and the Seven Dwarfs",
            "Gone with the Wind",
            "Casablanca",
            "The Bridge on the River Kwai",
            "Some Like It Hot",
        };
        File.WriteAllLines(filename, movies);

        string[] moreMovies =
        {
            "Psycho",
            "Easy Rider",
            "Pulp Fiction",
            "Star Wars",
            "The Matrix",
        };
        File.AppendAllLines(filename, moreMovies);
        ReadLineByLine(filename);

        Console.WriteLine();
    }

    private static void ReadLineByLine()
    {
        Console.WriteLine(nameof(ReadLineByLine));

        string? input;
        while (!string.IsNullOrWhiteSpace(input = GetFile()))
        {
            ReadLineByLine(input);
        }

        Console.WriteLine();

        static string? GetFile()
        {
            Console.Write($"Filename: ");
            return Console.ReadLine();
        }
    }

    private static void ReadLineByLine(string file)
    {
        IEnumerable<string> lines = File.ReadLines(file);
        int i = 1;
        foreach (string line in lines)
        {
            Console.WriteLine($"{i++}. {line}");
        }
    }

    private static void ChangeFileProperties()
    {
        Console.WriteLine(nameof(ChangeFileProperties));

        string? input;
        while (!string.IsNullOrWhiteSpace(input = GetFile()))
        {
            ChangeFileProperties(input);
        }

        Console.WriteLine();

        static string? GetFile()
        {
            Console.Write($"Filename: ");
            return Console.ReadLine();
        }
    }

    private static void ChangeFileProperties(string file)
    {
       
        FileInfo fileInfo = new(file);
        if (!fileInfo.Exists)
        {
            Console.WriteLine($"File {file} does not exist.");
            return;
        }

        Console.WriteLine($"Creation time: {fileInfo.CreationTime:F}");
        fileInfo.CreationTime = new(2035, 12, 24, 15, 0, 0);
        Console.WriteLine($"Creation time: {fileInfo.CreationTime:F}");
    }

    private static void FileInformation()
    {
        Console.WriteLine(nameof(FileInformation));

        string? input;
        while (!string.IsNullOrWhiteSpace(input = GetFile()))
        {
            FileInformation(input);
        }

        Console.WriteLine();

        static string? GetFile()
        {
            Console.Write($"Filename: ");
            return Console.ReadLine();
        }
    }

    private static void FileInformation(string file)
    {
        FileInfo fileInfo = new(file);
        if (!fileInfo.Exists)
        {
            Console.WriteLine($"File {file} not found.");
            return;
        }

        Console.WriteLine($"Name: {fileInfo.Name}");
        Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
        Console.WriteLine($"Read only: {fileInfo.IsReadOnly}");
        Console.WriteLine($"Extension: {fileInfo.Extension}");
        Console.WriteLine($"Length: {fileInfo.Length}");
        Console.WriteLine($"Creation time: {fileInfo.CreationTime}");
        Console.WriteLine($"Access time: {fileInfo.LastAccessTime}");
        Console.WriteLine($"File attributes: {fileInfo.Attributes}");
    }

    private static void CreateFile()
    {
        Console.WriteLine(nameof(CreateFile));

        string? input;
        while (!string.IsNullOrWhiteSpace(input = GetFile()))
        {
            CreateFile(input);
        }

        Console.WriteLine();

        static string? GetFile()
        {
            Console.Write($"Filename: ");
            return Console.ReadLine();
        }
    }

    private static void CreateFile(string file)
    {
        try
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), file);
            File.WriteAllText(path, "Hello, World");
            Console.WriteLine($"created file {path}");
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"Invalid characters in file name");
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message );
        }
    }

    private static void  ShowSpecialFolders()
    {
        Console.WriteLine(nameof(ShowSpecialFolders));

        foreach (string specialFolder in Enum.GetNames(typeof(Environment.SpecialFolder)))
        {
            Environment.SpecialFolder folder = Enum.Parse<Environment.SpecialFolder>(specialFolder);
            string path = Environment.GetFolderPath(folder);
            Console.WriteLine($"{specialFolder}: {path}");
        }

        Console.WriteLine();
    }

    private static void ShowDrives()
    {
        Console.WriteLine(nameof(ShowDrives));

        DriveInfo[] drives = DriveInfo.GetDrives();
        foreach (DriveInfo drive in drives)
        {
            if (drive.IsReady)
            {
                Console.WriteLine($"Drive name: {drive.Name}");
                Console.WriteLine($"Format: {drive.DriveFormat}");
                Console.WriteLine($"Type: {drive.DriveType}");
                Console.WriteLine($"Root directory: {drive.RootDirectory}");
                Console.WriteLine($"Volume label: {drive.VolumeLabel}");
                Console.WriteLine($"Free space: {drive.TotalFreeSpace}");
                Console.WriteLine($"Available space: {drive.AvailableFreeSpace}");
                Console.WriteLine($"Total size: {drive.TotalSize}");
                Console.WriteLine();
            }
        }
    }
}
