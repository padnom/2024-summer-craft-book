namespace PasswordValidation;
public sealed class Password
{
    public const string EmptyPasswordError = "Password cannot be empty.";
    public const string MinLengthError = "Password must be at least 8 characters long.";
    public const string UpperCaseLetterError = "Password must contain at least one uppercase letter.";
    public const string LowerCaseLetterError = "Password must contain at least one lowercase letter.";
    public const string DigitError = "Password must contain at least one digit.";
    public const string InvalidCharacterError = "Password contains invalid special characters.";
    private const int MinLenghtValue = 8;
    private static readonly List<char> _specialCharacters = new() { '.', '*', '#', '@', '$', '%', '&', };
    public static readonly string SpecialCharacterError =
        $"Password must contain at least one of the following special characters: {string.Join(", ", _specialCharacters)}";

    public static Result<Password> CreatePassword(string value)
    {
        IEnumerable<string> errors = ValidatePassword(value,
                                                      v => !string.IsNullOrWhiteSpace(v) ? null : EmptyPasswordError,
                                                      v => HasMinLenght(v) ? null : MinLengthError,
                                                      v => HasUpperCaseLetter(v) ? null : UpperCaseLetterError,
                                                      v => HasLowerCaseLetter(v) ? null : LowerCaseLetterError,
                                                      v => HasDigit(v) ? null : DigitError,
                                                      v => HasSpecialCharacter(v) ? null : SpecialCharacterError,
                                                      v => !HasInvalidCharacter(v) ? null : InvalidCharacterError
        );

        return errors.Any() ? Result<Password>.Failure(errors) : Result<Password>.Success();
    }

    private static bool HasDigit(string value) => value.Any(char.IsDigit);

    private static bool HasInvalidCharacter(string value) => value.Any(c => !_specialCharacters.Contains(c) && !char.IsLetterOrDigit(c));

    private static bool HasLowerCaseLetter(string value) => value.Any(char.IsLower);

    private static bool HasMinLenght(string value) => value.Length >= MinLenghtValue;

    private static bool HasSpecialCharacter(string value) => value.Any(_specialCharacters.Contains);

    private static bool HasUpperCaseLetter(string value) => value.Any(char.IsUpper);

    private static IEnumerable<string> ValidatePassword(string value, params Func<string, string?>[] validationRules)
    {
        return validationRules
               .Select(rule => rule(value))
               .OfType<string>();
    }
}