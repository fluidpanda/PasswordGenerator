namespace PasswordGenerator;

/// <summary>
/// Generates passwords.
/// </summary>
internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: password <length>");
            Console.Error.WriteLine("Usage: password entropy <length>");
            return;
        }
        if (args[0] == "entropy" && args.Length == 1)
        {
            Console.Error.WriteLine("Usage: password entropy <length>");
            return;
        }
        if (args[0] == "entropy" && int.TryParse(args[1], out var entropy))
        {
            PasswordFromEntropy(entropy);
            return;
        }
        if (args[0] == "entropy" && !int.TryParse(args[1], out var entropySize))
        {
            Console.Error.WriteLine($"Cannot parse entropy size: {args[1]}");
            return;
        }
        if (!int.TryParse(args[0], out var length) || length <=2)
        {
            Console.Error.WriteLine($"Cannot parse length `{args[0]}`, use positive int > 2");
            return;
        }
        PasswordFromLength(length);
    }

    private static void PrintUsage()
    {
        Console.Error.WriteLine("Usage: password <length>");
        Console.Error.WriteLine("Usage: password entropy <length>");
    }

    private static bool GetArgs(string[] args, out string? arg)
    {
        arg = args.Length > 1 ? args[1] : null;
        return arg is not null;
    }

    private static void PasswordFromLength(int length)
    {
        var (password, elapsedMs) = PasswordGenerator.GeneratePassword(length);
        Console.WriteLine(password);
        Console.WriteLine($"Elapsed: {elapsedMs} ms");
        Console.WriteLine($"Entropy: {PasswordGenerator.CalculateEntropy(length, PasswordGenerator.Length):F2} bit");
    }

    private static void PasswordFromEntropy(int entropy)
    {
        var length = PasswordGenerator.LengthFromEntropy(entropy, PasswordGenerator.Length);
        var (password, elapsedMs) = PasswordGenerator.GeneratePassword(length);
        Console.WriteLine(password);
        Console.WriteLine($"Elapsed: {elapsedMs} ms");
        Console.WriteLine($"Length: {password.Length} chars");
    }
}
