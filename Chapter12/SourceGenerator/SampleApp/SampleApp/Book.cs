﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeGenerationSample;

namespace SampleApp;

[ImplementEquatable]
public partial class Book
{
    public Book(string title, string publisher)
    {
        Title = title;
        Publisher = publisher;
    }

    public string Title { get; }
    public string Publisher { get; }

    private static partial bool IsTheSame(Book? left, Book? right)
        => left?.Title == right?.Title && left?.Publisher == right?.Publisher;

    public override int GetHashCode() => HashCode.Combine(Title, Publisher);
}
