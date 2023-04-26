using System.Diagnostics;

namespace SortingSample;

public enum PersonCompareType { FirstName, LastName }

public class PersonComparer : IComparer<Person>
{
    private readonly PersonCompareType _compareType;

    public PersonComparer(PersonCompareType compareType) => _compareType = compareType;

    public int Compare(Person? x, Person? y) => x is null && y is null
                                                ? 0
                                                : x is null
                                                  ? 1
                                                  : y is null
                                                    ? -1
                                                    : _compareType switch
                                                    {
                                                        PersonCompareType.FirstName
                                                            => x.FirstName.CompareTo(y.FirstName),
                                                        PersonCompareType.LastName
                                                            => x.LastName.CompareTo(y.LastName),
                                                        _ => throw new UnreachableException()
                                                    };
}
