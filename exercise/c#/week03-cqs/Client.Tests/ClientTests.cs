using FluentAssertions;
using Xunit;

namespace Client.Tests;
public sealed class ClientTests
{
    private readonly Accountability.Client _client = new(new Dictionary<string, double>
                                                         {
                                                             { "Tenet Deluxe Edition", 45.99 },
                                                             { "Inception", 30.50 },
                                                             { "The Dark Knight", 30.50 },
                                                             { "Interstellar", 23.98 },
                                                         });

    [Fact]
    public void Client_Should_Return_Statement()
    {
        // Normalize newlines in the actual statement
        string normalizedActualStatement = _client.ToStatement().Replace("\r\n", "\n");

        // Define and normalize the expected statement
        string normalizedExpectedStatement =
            """
                Tenet Deluxe Edition for 45.99€
                Inception for 30.5€
                The Dark Knight for 30.5€
                Interstellar for 23.98€
                Total : 130.97€
                """.Replace("\r\n", "\n");

        // Perform the assertion with normalized strings
        normalizedActualStatement.Should().BeEquivalentTo(normalizedExpectedStatement);
    }

    [Fact]
    public void Client_Should_Return_Total_Amount_When_Called_Twice()
    {
        // Act
        _ = _client.ToStatement();
        _ = _client.ToStatement();

        // Assert
        _client.TotalAmount().Should().Be(130.97);
    }
}