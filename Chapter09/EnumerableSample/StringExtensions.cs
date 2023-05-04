namespace EnumerableSample;
public static class StringExtensions
{
    public static string FirstName(this string name) => name[..name.LastIndexOf(' ')];

    public static string LasttName(this string name) => name[(name.LastIndexOf(' ') + 1)..];
}
