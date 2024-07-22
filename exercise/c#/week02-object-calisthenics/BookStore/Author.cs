namespace BookStore;
public sealed class Author
{
    public bool IsValid => !string.IsNullOrWhiteSpace(Value);
    public string Value { get; }

    private Author(string value) => Value = value;

    public static Author CreateAuthor(string authorName) => !string.IsNullOrWhiteSpace(authorName) ? new Author(authorName) : CreateEmptyAuthor();

    public static Author CreateEmptyAuthor() => new(string.Empty);
}