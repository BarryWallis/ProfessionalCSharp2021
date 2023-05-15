// See https://aka.ms/new-console-template for more information

using PInvokeSampleLib;

if (args.Length != 2)
{
    Console.WriteLine($"usage: PInvokeSample existingFilename newFilename");
    return;
}

try
{
    FileUtility.CreateHardLink(args[0], args[1]);
}
catch (IOException ex)
{
    Console.WriteLine(ex.Message);
}
