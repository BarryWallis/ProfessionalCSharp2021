using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSample;

public record Card(string Title)
{
    public IList<Category> Categories { get; init; } = new List<Category>();
}
