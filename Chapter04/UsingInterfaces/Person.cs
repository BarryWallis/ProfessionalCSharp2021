namespace UsingInterfaces;
public record Person(string FirstName, string LastName) : IComparable<Person>
{
    public int CompareTo(Person? other)
    {
        int compare = LastName.CompareTo(other?.LastName);
        return compare is 0 ? FirstName.CompareTo(other?.FirstName) : compare;
    }
}
