namespace ClassesSample;
public static class PersonExtensions
{
    public static void Deconstruct(this Person person, out string firstName, out string lastName, out int age)
    {
        firstName = person.FirstName;
        lastName = person.LastName;
        age = person.Age;
    }
}
