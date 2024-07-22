namespace BookStore;
public sealed class Title
{
    public readonly string Value;
    public bool IsValid => !string.IsNullOrWhiteSpace(Value);

    private Title(string value) => Value = value;

    public static Title CreateEmptyTitle() => new(string.Empty);

    public static Title CreateTitle(string title) => !string.IsNullOrWhiteSpace(title) ? new Title(title) : CreateEmptyTitle();
}