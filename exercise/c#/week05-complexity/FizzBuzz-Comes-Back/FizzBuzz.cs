namespace FizzBuzz;
public static class FizzBuzz
{
    private const int Min = 1;
    private const int Max = 100;
    private static readonly Dictionary<int, string> _rules =
        new()
        {
            { 15, "FizzBuzz" },
            { 3, "Fizz" },
            { 5, "Buzz" },
        };

    public static string Convert(int input)
    {
        ValidateInput(input);

        return ConvertSafely(input);
    }

    private static string ConvertSafely(int input) => _rules.FirstOrDefault(rule => input % rule.Key == 0).Value ?? input.ToString();

    private static void ValidateInput(int input)
    {
        if (input is < Min or > Max)
        {
            throw new OutOfRangeException();
        }
    }
}