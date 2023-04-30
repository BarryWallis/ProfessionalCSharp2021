// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;

ImmutableArray<string> a1 = ImmutableArray.Create<string>();
ImmutableArray<string> a2 = a1.Add("Williams");
ImmutableArray<string> a3 = a2.Add("Ferrari").Add("Mercedes").Add("Red Bull Racing");

List<Account> accounts = new()
{
    new("Scrooge McDuck", 667377678765M),
    new("Donald Duck", -200M),
    new("Ludwig von Drake", 20_000M),
};
ImmutableList<Account> immutableAccounts = accounts.ToImmutableList();
foreach (Account account in immutableAccounts)
{
    Console.WriteLine($"{account.Name} {account.Amount}");
}
Console.WriteLine();

immutableAccounts.ForEach(account => Console.WriteLine($"{account.Name} {account.Amount}"));
Console.WriteLine();

ImmutableList<Account>.Builder builder = immutableAccounts.ToBuilder();
for (int i = builder.Count - 1; i >= 0; i--)
{
    Account a = builder[i];
    if (a.Amount > 0)
    {
        _ = builder.Remove(a);
    }
}
ImmutableList<Account> overdrawnAccounts = builder.ToImmutable();
overdrawnAccounts.ForEach(account => Console.WriteLine($"overdrawn: {account.Name} {account.Amount}"));
Console.WriteLine();

#pragma warning disable CA1050 // Declare types in namespaces
public record Account(string Name, decimal Amount);
#pragma warning restore CA1050 // Declare types in namespaces
