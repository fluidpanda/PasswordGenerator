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
            PrintUsage();
            return;
        }
        var cmd = args[0].ToLowerInvariant();
        switch (cmd)
        {
            case "entropy":
                if (!GetArgs(args, out var arg1))
                {
                    Console.Error.WriteLine("Usage: password entropy <length>");
                    return;
                }
                if (!int.TryParse(arg1, out var entropy))
                {
                    Console.Error.WriteLine($"Cannot parse entropy size: {arg1}");
                    return;
                }
                PasswordFromEntropy(entropy);
                break;
            default:
                if (!int.TryParse(cmd, out var length)|| length <= 2)
                {
                    Console.Error.WriteLine($"Cannot parse length `{cmd}`, use positive int > 2");
                    return;
                }
                PasswordFromLength(length);
                return;
        }
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
