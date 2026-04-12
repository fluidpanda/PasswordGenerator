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
			return;
		}
		if (!int.TryParse(args[0], out var length) || length <=0)
		{
			Console.Error.WriteLine($"Cannot parse length {args[0]}, use positive int");
			return;
		}
		var (password, elapsedMs) = PasswordGenerator.GeneratePassword(length);
		Console.WriteLine(password);
		Console.WriteLine($"Elapsed: {elapsedMs} ms");
		Console.WriteLine($"Entropy: {PasswordGenerator.CalculateEntropy(length, PasswordGenerator.Length):F2} bit");
	}
}
