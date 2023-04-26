﻿namespace CustomIndexerSample;
public record Person(string FirstName, string LastName, DateTime Birthday)
{
    public override string? ToString() => $"{FirstName} {LastName}";
}
