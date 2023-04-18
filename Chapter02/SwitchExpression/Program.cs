// See https://aka.ms/new-console-template for more information

using static TrafficLight;
#pragma warning disable IDE0062 // Make local function 'static'
#pragma warning disable CS8321 // Local function is declared but never used
TrafficLight NextLightClassic(TrafficLight light)
{
#pragma warning disable IDE0066 // Convert switch statement to expression
    switch (light)
    {
        case Green:
            return Amber;
        case Amber:
            return Red;
        case Red:
            return Green;
        default:
            throw new InvalidOperationException();
    }
#pragma warning restore IDE0066 // Convert switch statement to expression
}

TrafficLight NextLight(TrafficLight light) =>
    light switch
    {
        Green => Amber,
        Amber => Red,
        Red => Green,
        _ => throw new InvalidOperationException()
    };

string? input = Console.ReadLine();

#pragma warning disable IDE0059 // Unnecessary assignment of a value
string result = input switch
{
    "one" => "the input has the value one",
    "two" or "three" => "the input has the value two or three",
    _ => "any other value"
};
#pragma warning restore IDE0059 // Unnecessary assignment of a value

#pragma warning disable IDE0040 // Add accessibility modifiers
enum TrafficLight
#pragma warning restore IDE0040 // Add accessibility modifiers
{
    Red,
    Amber,
    Green,
}

#pragma warning restore CS8321 // Local function is declared but never used
#pragma warning restore IDE0062 // Make local function 'static'
