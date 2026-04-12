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
}
