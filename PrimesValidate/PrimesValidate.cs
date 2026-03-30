using System.Numerics;

var inputPath = args.Length > 0 ? args[0] : "primes.txt";
if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Missing file: {inputPath}");
    Environment.Exit(1);
}

BigInteger? previous = null;
var lineNumber = 0;

foreach (var line in File.ReadLines(inputPath))
{
    lineNumber++;
    if (!BigInteger.TryParse(line, out var value))
    {
        Console.Error.WriteLine($"Invalid number at line {lineNumber}: {line}");
        Environment.Exit(1);
    }

    if (value < 2)
    {
        Console.Error.WriteLine($"Non-prime value at line {lineNumber}: {value}");
        Environment.Exit(1);
    }

    if (previous.HasValue && value <= previous.Value)
    {
        Console.Error.WriteLine($"Out of order value at line {lineNumber}: {value}");
        Environment.Exit(1);
    }

    if (!IsPrime(value))
    {
        Console.Error.WriteLine($"Composite value at line {lineNumber}: {value}");
        Environment.Exit(1);
    }

    previous = value;
}

Console.WriteLine("primes.txt is valid.");

static bool IsPrime(BigInteger value)
{
    if (value == 2)
    {
        return true;
    }

    if (value < 2 || value % 2 == 0)
    {
        return false;
    }

    for (var i = new BigInteger(3); i * i <= value; i += 2)
    {
        if (value % i == 0)
        {
            return false;
        }
    }

    return true;
}
