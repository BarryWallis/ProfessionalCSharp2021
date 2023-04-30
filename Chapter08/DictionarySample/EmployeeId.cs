using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DictionarySample;

public class EmployeeIdException : Exception
{
    public EmployeeIdException(string? message) : base(message)
    {
    }
}

public readonly struct EmployeeId : IEquatable<EmployeeId>
{
    private readonly char _prefix;
    private readonly int _number;

    public EmployeeId(string id)
    {
        _prefix = id.ToUpper()[0];
        int last = id.Length > 7 ? 7 : id.Length;
        try
        {
            _number = int.Parse(id[1..last]);
        }
        catch (FormatException)
        {
            throw new EmployeeIdException("Invalid EmployeeId format");
        }
    }

    public bool Equals(EmployeeId other) => _prefix == other._prefix && _number == other._number;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        Debug.Assert(obj is EmployeeId);
        return Equals((EmployeeId)obj);
    }

    public override string ToString() => $"{_prefix}{_number,6:000000}";

    public override int GetHashCode() => (_number ^ (_number << 16)) * 0x1505_1505;

    public static bool operator ==(EmployeeId left, EmployeeId right) => left.Equals(right);

    public static bool operator !=(EmployeeId left, EmployeeId right) => !(left == right);
}
