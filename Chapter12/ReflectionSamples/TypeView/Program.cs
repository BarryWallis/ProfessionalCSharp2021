// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Text;

StringBuilder outputText = new();
Type t = typeof(TypeInfo);
AnalyzeType(t);
Console.WriteLine($"Analysis of type {t.Name}");
Console.WriteLine(outputText.ToString());

void AnalyzeType(Type t)
{
    TypeInfo typeInfo = t.GetTypeInfo();
    AddToOutput($"Type Name: {t.Name}");
    AddToOutput($"Full Name: {t.FullName}");
    AddToOutput($"Namespace: {t.Namespace}");

    Type? tBase = typeInfo.BaseType;
    if (tBase is not null)
    {
        AddToOutput($"Base Type: {tBase.Name}");
    }

    ShowMembers("constructors", t.GetConstructors());
    ShowMembers("methods", t.GetMethods());
    ShowMembers("properties", t.GetProperties());
    ShowMembers("fields", t.GetFields());
    ShowMembers("events", t.GetEvents());
}

void ShowMembers(string title, IList<MemberInfo> members)
{
    if (members.Count == 0)
    {
        return;
    }
    
    AddToOutput($"\npublic {title}");
    IEnumerable<string> names = members.Select(m => m.Name).Distinct();
    AddToOutput(string.Join(" ", names));
}

void AddToOutput(string text) => outputText.AppendLine(text);
