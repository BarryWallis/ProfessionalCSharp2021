using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSample;

public record Category(string Title)
{
    public IList<Item> Items { get; init; } = new List<Item>();
}
