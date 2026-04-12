namespace PasswordGenerator.Tests;

public class PasswordGeneratorTests
{
    [Fact]
    public void PasswordShouldContainAllChars()
    {
        var (password, _) = PasswordGenerator.GeneratePassword(16);
        Assert.Contains(password, char.IsLower);
        Assert.Contains(password, char.IsUpper);
        Assert.Contains(password, char.IsDigit);
    }

    [Fact]
    public void PasswordLengthFromEntropy()
    {
        const int passwordLength = 32;
        var charsetSize = PasswordGenerator.Length;
        var entropy = PasswordGenerator.CalculateEntropy(passwordLength, charsetSize);
        var lengthFromEntropy = PasswordGenerator.LengthFromEntropy(entropy, charsetSize);
        Assert.Equal(passwordLength, lengthFromEntropy);
    }
}
