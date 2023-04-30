using System.Diagnostics;

namespace ListSamples;
public class RacerComparer : IComparer<Racer>
{
    public enum CompareType { FirstName, LastName, Country, Wins, }

    private readonly CompareType _compareType;

    public RacerComparer(CompareType compareType) => _compareType = compareType;

    public int Compare(Racer? x, Racer? y)
    {
        return x is null && y is null
            ? 0
            : x is null
            ? -1
            : y is null
            ? 1
            : _compareType switch
            {
                CompareType.FirstName => string.Compare(x.FirstName, y.FirstName),
                CompareType.LastName => string.Compare(x.LastName, y.LastName),
                CompareType.Country => CompareCountry(x, y),
                CompareType.Wins => x.Wins.CompareTo(y.Wins),
                _ => throw new UnreachableException($"Invalid {nameof(_compareType)}: {_compareType}")
            };

        static int CompareCountry(Racer x, Racer y)
        {
            int result = string.Compare(x.Country, y.Country);
            if (result == 0)
            {
                result = string.Compare(x.LastName, y.LastName);
            }

            return result;
        }
    }
}
