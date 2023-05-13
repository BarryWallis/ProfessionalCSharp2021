// See https://aka.ms/new-console-template for more information

using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;

using WhatsNewAttributes;

StringBuilder outputText = new(1000);
DateTime backDateTo = new(2019, 2, 1);

Assembly assembly = Assembly.Load(new AssemblyName("VectorClass"));
AddToOutput($"Assembly: {assembly.FullName}");
Attribute? supportsAttribute = assembly.GetCustomAttribute(typeof(SupportsWhatsNewAttribute));
if (supportsAttribute is null)
{
    Console.WriteLine($"This assembly does not support WhatsNew attributes");
    return;
}
else
{
    AddToOutput("Defined Types:");
}

IEnumerable<Type> types = assembly.ExportedTypes;
foreach (Type type in types)
{
    DisplayTypeInfo(type);
}

Console.WriteLine($"What's New since {backDateTo:D}");
Console.WriteLine(outputText.ToString());

void DisplayTypeInfo(Type type)
{
    if (!type.GetTypeInfo().IsClass)
    {
        return;
    }
    AddToOutput($"{Environment.NewLine}class {type.Name}");

    IEnumerable<LastModifiedAttribute> lastModifiedAttributes
        = type.GetTypeInfo()
              .GetCustomAttributes()
              .OfType<LastModifiedAttribute>()
              .Where(a => a.DateModified >= backDateTo)
              .ToArray();
    if (!lastModifiedAttributes.Any())
    {
        AddToOutput($"\tNo changes to the class {type.Name}{Environment.NewLine}");
    }
    else
    {
        foreach (LastModifiedAttribute lastModifiedAttribute in lastModifiedAttributes)
        {
            WriteAttributeInfo(lastModifiedAttribute);
        }
    }

    AddToOutput($"changes to methods of this class");
    foreach (MethodInfo methodInfo in type.GetTypeInfo().DeclaredMembers.OfType<MethodInfo>())
    {
        IEnumerable<LastModifiedAttribute> attributesToMethods
            = methodInfo.GetCustomAttributes()
                        .OfType<LastModifiedAttribute>()
                        .Where(a => a.DateModified >= backDateTo)
                        .ToArray();
        if (attributesToMethods.Any())
        {
            AddToOutput($"{methodInfo.ReturnType} {methodInfo.Name}()");
            foreach (Attribute attribute in attributesToMethods)
            {
                WriteAttributeInfo(attribute);
            }
        }
    }
}

void WriteAttributeInfo(Attribute attribute)
{
    if (attribute is LastModifiedAttribute lastModifiedAttribute)
    {
        AddToOutput($"\tmodified: {lastModifiedAttribute.DateModified:D}: {lastModifiedAttribute.Changes}");
        if (!string.IsNullOrEmpty(lastModifiedAttribute.Issues))
        {
            AddToOutput($"\tOutstanding issues: {lastModifiedAttribute.Issues}");
        }
    }
}

void AddToOutput(string text) => outputText.Append($"{Environment.NewLine}{text}");
