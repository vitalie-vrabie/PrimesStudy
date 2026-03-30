using System.Numerics;

var primes = new List<BigInteger>();
var outputPath = "primes.txt";

if (File.Exists(outputPath))
{
    foreach (var line in File.ReadLines(outputPath))
    {
        if (BigInteger.TryParse(line, out var prime))
        {
            primes.Add(prime);
        }
    }
}

if (primes.Count == 0)
{
    primes.Add(2);
    Console.WriteLine(2);
    File.WriteAllText(outputPath, "2" + Environment.NewLine);
}

using var writer = new StreamWriter(outputPath, append: true) { AutoFlush = true };
var stopRequested = false;
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    stopRequested = true;
};

var lastPrime = primes[^1];
var candidate = lastPrime <= 2 ? 3 : lastPrime + 2;
if (candidate % 2 == 0)
{
    candidate += 1;
}

for (; ; candidate += 2)
{
    if (stopRequested)
    {
        break;
    }

    var isPrime = true;

    foreach (var prime in primes)
    {
        if (prime * prime > candidate)
        {
            break;
        }

        if (candidate % prime == 0)
        {
            isPrime = false;
            break;
        }
    }

    if (isPrime)
    {
        primes.Add(candidate);
        Console.WriteLine(candidate);
        writer.WriteLine(candidate);
    }
}
