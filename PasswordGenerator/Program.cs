using System.Diagnostics;
using System.Security.Cryptography;

namespace PasswordGenerator;

/// <summary>
/// Generates passwords.
/// </summary>
internal static class Program
{
	private const string Lower = "abcdefghijklmnopqrstuvwxyz";
	private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private const string Digits = "0123456789";
	private const string Charset = Lower + Upper + Digits;
	private static readonly int Length = Charset.Length;
	
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
		var (password, elapsedMs) = GeneratePassword(length);
		Console.WriteLine(password);
		Console.WriteLine($"Elapsed: {elapsedMs} ms");
		Console.WriteLine($"Entropy: {CalculateEntropy(length, Length):F2} bit");
	}

	private static (string Password, long ElapsedMs) GeneratePassword(int length)
	{
		var sw = Stopwatch.StartNew();
		var array = new char[length];
		for (var i = 0; i < length; i++)
		{
			array[i] = Charset[RandomNumberGenerator.GetInt32(0, Charset.Length)];
		}
		var password = new string(array);
		sw.Stop();
		var elapsedMs = sw.ElapsedMilliseconds;
		return (password, elapsedMs);
	}

	private static double CalculateEntropy(int length, int charsetSize)
	{
		var result = length * Math.Log2(charsetSize);
		return result;
	}
}
