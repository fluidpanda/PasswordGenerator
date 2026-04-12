using System.Diagnostics;
using System.Security.Cryptography;

namespace PasswordGenerator;

public static class PasswordGenerator
{
    private const string Lower = "abcdefghijklmnopqrstuvwxyz";
    private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string Charset = Lower + Upper + Digits;
    public static readonly int Length = Charset.Length;
    
    public static (string Password, long ElapsedMs) GeneratePassword(int length)
    {
        var sw = Stopwatch.StartNew();
        var lowerChar = Lower[RandomNumberGenerator.GetInt32(0, Lower.Length)];
        var upperChar = Upper[RandomNumberGenerator.GetInt32(0, Upper.Length)];
        var digitChar = Digits[RandomNumberGenerator.GetInt32(0, Digits.Length)];
        var array = new char[length];
        array[0] = lowerChar;
        array[1] = upperChar;
        array[2] = digitChar;
        for (var i = 3; i < length; i++)
        {
            array[i] = Charset[RandomNumberGenerator.GetInt32(0, Charset.Length)];
        }
        Shuffle(array);
        var password = new string(array);
        sw.Stop();
        var elapsedMs = sw.ElapsedMilliseconds;
        return (password, elapsedMs);
    }

    public static double CalculateEntropy(int length, int charsetSize)
    {
        var result = length * Math.Log2(charsetSize);
        return result;
    }

    private static void Shuffle(char[] items)
    {
        var n = items.Length;
        while (n > 1)
        {
            n--;
            var k = RandomNumberGenerator.GetInt32(0, n + 1);
            (items[n], items[k]) = (items[k], items[n]);
        }
    }
}