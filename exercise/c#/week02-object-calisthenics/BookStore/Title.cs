namespace BookStore;
public sealed class Title
{
    public bool IsValid => !string.IsNullOrWhiteSpace(Value);
    public string Value { get; }

    private Title(string value)
    {
        Value = value;
    }

    public static Title CreateEmptyTitle()
    {
        return new Title(string.Empty);
    }

    public static Title CreateTitle(string title)
    {
        return !string.IsNullOrWhiteSpace(title) ? new Title(title) : CreateEmptyTitle();
    }
}