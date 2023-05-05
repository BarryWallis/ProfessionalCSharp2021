using RethrowExceptions;

HandleAll();

#line 100
static void HandleAll()
{
    Action[] methods =
    {
                HandleAndThrowAgain,
                HandleAndThrowWithInnerException,
                HandleAndRethrow,
                HandleWithFilter,
    };

    foreach (Action method in methods)
    {
        try
        {
            method();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            if (ex.InnerException is not null)
            {
                Console.WriteLine($"\tInner Exception: {ex.InnerException.Message}");
                Console.WriteLine(ex.InnerException.StackTrace);
            }
            Console.WriteLine();
        }
    }

#line 1000
    static void HandleWithFilter()
    {
        try
        {
            ThrowAnException("test 4");
        }
        catch (Exception ex) when (Filter(ex))
        {
            Console.WriteLine("block never invoked");
        }
    }

#line 1500
    static bool Filter(Exception ex)
    {
        Console.WriteLine($"just log {ex.Message}");
        return false;
    }

#line 2000
    static void HandleAndRethrow()
    {
        try
        {
            ThrowAnException("test 3");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Log exception {ex.Message} and rethrow");
            throw;
        }
    }

#line 3000
    static void HandleAndThrowWithInnerException()
    {
        try
        {
            ThrowAnException("test2");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Log exception {ex.Message} and throw again");
            throw new AnotherCustomException("throw with inner exception", ex);
            throw;
        }
    }

#line 4000
    static void HandleAndThrowAgain()
    {
        try
        {
            ThrowAnException("test 1");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Log exception {ex.Message} and throw again");
#pragma warning disable CA2200 // Rethrow to preserve stack details
            throw ex;
#pragma warning restore CA2200 // Rethrow to preserve stack details
        }
    }

#line 8000
    static void ThrowAnException(string message) => throw new MyCustomException(message);
}
