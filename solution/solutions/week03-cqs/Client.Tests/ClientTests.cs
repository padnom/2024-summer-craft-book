using FluentAssertions;
using Xunit;

namespace Client.Tests
{
    public class ClientTests
    {
        private readonly Accountability.Client _client = new(new Dictionary<string, double>
        {
            {"Tenet Deluxe Edition", 45.99},
            {"Inception", 30.50},
            {"The Dark Knight", 30.50},
            {"Interstellar", 23.98}
        });

        [Fact]
        public void Client_Should_Return_Statement()
            => _client
                .ToStatement()
                .Should()
                .BeEquivalentTo(
                    """
                    Tenet Deluxe Edition for 45.99€
                    Inception for 30.5€
                    The Dark Knight for 30.5€
                    Interstellar for 23.98€
                    Total : 130.97€
                    """);

        [Fact]
        public void Client_Should_Calculate_TotalAmount()
            => _client
                .TotalAmount()
                .Should()
                .Be(130.97);
    }
}