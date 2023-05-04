namespace DataLib;
public record Team
{
    public string Name { get; }
    public IEnumerable<int> Years { get; }

    public Team(string name, params int[] years)
    {
        Name = name;
        Years = years;
    }
}
