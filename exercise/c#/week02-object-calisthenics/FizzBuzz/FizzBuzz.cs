namespace FizzBuzz;
public static class FizzBuzz
{
    public static string Convert(int input)
    {
        ValidateInput(input);

        return input switch
               {
                   _ when IsDivisibleBy(input, 15) => "FizzBuzz",
                   _ when IsDivisibleBy(input, 3)  => "Fizz",
                   _ when IsDivisibleBy(input, 5)  => "Buzz",
                   _                               => input.ToString(),
               };
    }

    private static bool IsDivisibleBy(int number, int divisor)
    {
        return number % divisor == 0;
    }

    private static void ValidateInput(int input)
    {
        if (input <= 0
            || input > 100)
            throw new OutOfRangeException();
    }
}