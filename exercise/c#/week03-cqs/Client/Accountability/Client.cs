using static System.Environment;
using static System.Globalization.CultureInfo;
using static System.String;

namespace Client.Accountability;
public sealed class Client(IReadOnlyDictionary<string, double> orderLines)
{
    public string ToStatement() => $"{FormatLines()}{AddLineTotal()}";

    private string FormatLines()
    {
        return Join(
            NewLine,
            orderLines
                .Select(kvp => FormatLine(kvp.Key, kvp.Value))
                .ToList()
        );
    }

    public double TotalAmount() => orderLines.Values.Sum();

    private static string FormatLine(string name, double value) => name + " for " + value.ToString(InvariantCulture) + "€";

    private string AddLineTotal() => $"{NewLine}Total : {TotalAmount().ToString(InvariantCulture)}€";
}