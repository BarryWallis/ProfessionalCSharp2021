// See https://aka.ms/new-console-template for more information

using SolicitColdCall;

Console.Write($"Please type in the name of the file containing the names of the people to be cold called> ");
string? fileName = Console.ReadLine();
if (fileName is not null)
{
    ColdCallFileReaderLoop1(fileName);
    Console.WriteLine();
}

static void ColdCallFileReaderLoop1(string fileName)
{
    ColdCallFileReader peopleToRing = new();
    try
    {
        peopleToRing.Open(fileName);
        for (int i = 0; i < peopleToRing.NumberOfPeopleToRing; i++)
        {
            peopleToRing.ProcessNextPerson();
        }
        Console.WriteLine($"All callers processed correctly");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"The file {fileName} does not exist");
    }
    catch (ColdCallFileFormatException ex)
    {
        Console.WriteLine($"The file {fileName} appears to have been corrupted");
        Console.WriteLine($"Details of problem are: {ex.Message}");
        if (ex.InnerException is not null)
        {
            Console.WriteLine($"Inner exception was: {ex.InnerException.Message}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception occurred: \n{ex.Message}");
    }
    finally
    {
        peopleToRing.Dispose();
    }
}
