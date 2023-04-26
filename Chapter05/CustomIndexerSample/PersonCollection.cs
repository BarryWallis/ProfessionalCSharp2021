namespace CustomIndexerSample;
public class PersonCollection
{
    private readonly Person[] _people;

    public PersonCollection(params Person[] people) => _people = people.ToArray();

    public Person this[int index]
    {
        get => _people[index];
        set => _people[index] = value;
    }

    public IEnumerable<Person> this[DateTime birthday] => _people.Where(p => p.Birthday == birthday);
}
