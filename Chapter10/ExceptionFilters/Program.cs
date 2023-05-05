// See https://aka.ms/new-console-template for more information

using ExceptionFilters;

try
{
    ThrowWithErrorCode(405);
}
catch (MyCustomException ex) when (ex.ErrorCode == 405)
{
    Console.WriteLine($"Exception caught with filter {ex.Message}");
}
catch (MyCustomException ex)
{
    Console.WriteLine($"Exception caught {ex.Message} and {ex.ErrorCode}");
}
Console.WriteLine();

static void ThrowWithErrorCode(int errorCode) => throw new MyCustomException("Error in Foo")
{
    ErrorCode = errorCode
};
