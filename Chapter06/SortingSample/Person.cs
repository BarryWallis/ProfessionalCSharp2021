namespace SortingSample;
public record Person(string FirstName, string LastName) : IComparable<Person>
{
    public int CompareTo(Person? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result = string.Compare(LastName, other.LastName);
        if (result == 0)
        {
            result = string.Compare(FirstName, other.FirstName);
        }

        return result;
    }
}
