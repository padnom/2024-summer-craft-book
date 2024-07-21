using FluentAssertions.LanguageExt;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace FizzBuzz
{
    public class FizzBuzzTests
    {
        private static readonly string[] FizzBuzzStrings = ["Fizz", "Buzz", "FizzBuzz"];

        [Theory]
        [InlineData(1, "1")]
        [InlineData(67, "67")]
        [InlineData(82, "82")]
        [InlineData(3, "Fizz")]
        [InlineData(66, "Fizz")]
        [InlineData(99, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(50, "Buzz")]
        [InlineData(85, "Buzz")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(30, "FizzBuzz")]
        [InlineData(45, "FizzBuzz")]
        public void Returns_Number_Representation(int input, string expectedResult)
            => FizzBuzz.Convert(input)
                .Should()
                .BeSome(expectedResult);

        [Property]
        public void Parse_Return_Valid_String_For_Numbers_Between_1_And_100()
            => Prop.ForAll(
                ValidInput(),
                IsConvertValid
            ).QuickCheckThrowOnFailure();

        private static Arbitrary<int> ValidInput()
            => Gen.Choose(FizzBuzz.Min, FizzBuzz.Max).ToArbitrary();

        private static bool IsConvertValid(int x)
            => FizzBuzz.Convert(x).Exists(s => ValidStringsFor(x).Contains(s));

        private static IEnumerable<string> ValidStringsFor(int x)
            => FizzBuzzStrings.Append(x.ToString());

        [Property]
        public void ParseFailForNumbersOutOfRange()
            => Prop.ForAll(
                InvalidInput(),
                x => FizzBuzz.Convert(x).IsNone
            ).QuickCheckThrowOnFailure();

        private static Arbitrary<int> InvalidInput()
            => Gen.Choose(-10_000, 10_000)
                .ToArbitrary()
                .Filter(x => x is < FizzBuzz.Min or > FizzBuzz.Max);
    }
}