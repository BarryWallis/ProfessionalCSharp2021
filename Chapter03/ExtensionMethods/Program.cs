// See https://aka.ms/new-console-template for more information

using ExtensionMethods;

string fox = "the quick brown fox jumped over the lazy dogs";
int wordCount = fox.GetWordCount();
Console.WriteLine($"{wordCount} words");
Console.WriteLine();
