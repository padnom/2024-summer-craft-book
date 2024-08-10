using FluentAssertions;
using Xunit;

namespace PasswordValidation.Tests;
public class PasswordTest
{
    public static IEnumerable<object[]> InvalidPasswordTestData()
    {
        yield return CreateTestData("passwor", Password.MinLengthError, Password.UpperCaseLetterError, Password.DigitError, Password.SpecialCharacterError);
        yield return CreateTestData("password", Password.UpperCaseLetterError, Password.DigitError, Password.SpecialCharacterError);
        yield return CreateTestData("PASSWORD", Password.LowerCaseLetterError, Password.DigitError, Password.SpecialCharacterError);
        yield return CreateTestData("Password", Password.DigitError, Password.SpecialCharacterError);
        yield return CreateTestData("Password1", Password.SpecialCharacterError);
        yield return CreateTestData("Password!", Password.DigitError, Password.SpecialCharacterError, Password.InvalidCharacterError);
    }

    [Theory]
    [MemberData(nameof(InvalidPasswordTestData))]
    public void Password_ListErrors_IsCorrectlyPopulated(string passwordInput, string[] expectedErrors)
    {
        Result<Password> password = Password.CreatePassword(passwordInput);

        password.IsSuccess.Should().BeFalse();
        password.Errors.Should().BeEquivalentTo(expectedErrors);
    }

    [Fact]
    public void Success_For_A_Valid_Password()
    {
        Result<Password> password = Password.CreatePassword("Password123#");

        password.IsSuccess
                .Should()
                .BeTrue();
    }

    private static object[] CreateTestData(string passwordInput, params string[] expectedErrors) => [passwordInput, expectedErrors,];
}