using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFileReader;
public static class DynamicFileHelper
{
    public static IEnumerable<dynamic> ParseFile(string fileName)
    {
        List<dynamic> dataList = new();
        using StreamReader? streamReader = OpenFile(fileName);
        if (streamReader is not null)
        {
            string[] headerLine = streamReader.ReadLine()?.Split(',').Select(s => s.Trim()).ToArray()
                                  ?? throw new InvalidOperationException("reader.ReadLine() returned null");
            while (streamReader.Peek() > 0)
            {
                string[] dataLine = streamReader.ReadLine()?.Split(',', StringSplitOptions.TrimEntries)
                                    ?? throw new InvalidOperationException("reader.ReadLine() returned null");
                if (dataLine.Length != headerLine.Length)
                {
                    throw new InvalidDataException($"wrong number of fields in dataLine: " +
                        $"Expected {headerLine.Length} found {dataLine.Length}");
                }
                dynamic dynamicEntity = new ExpandoObject();
                for (int i = 0; i < headerLine.Length; i++)
                {
                    (dynamicEntity as IDictionary<string, object>)?.Add(headerLine[i], dataLine[i]);
                }
                dataList.Add(dynamicEntity);
            }
        }

        return dataList;
    }

    private static StreamReader? OpenFile(string fileName)
        => File.Exists(fileName) ? new StreamReader(fileName) : null;
}
