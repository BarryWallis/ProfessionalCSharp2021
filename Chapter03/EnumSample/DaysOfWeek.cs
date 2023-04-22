[Flags]
#pragma warning disable CA1050 // Declare types in namespaces
public enum DaysOfWeek
#pragma warning restore CA1050 // Declare types in namespaces
{
    Monday = 0x1,
    Tuesday = 0x2,
    Wednesday = 0x4,
    Thursday = 0x8,
    Friday = 0x10,
    Saturday = 0x20,
    Sunday = 0x40,
    Weekend = Saturday | Sunday,
    Workday = 0x1f,
    AllWeek = Weekend | Workday,
}
