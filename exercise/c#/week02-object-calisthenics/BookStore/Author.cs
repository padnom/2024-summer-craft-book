namespace BookStore;
public sealed class Author
{
    public readonly string Value;
    public bool IsValid => !string.IsNullOrWhiteSpace(Value);

    private Author(string value) => Value = value;

    public static Author CreateAuthor(string authorName) => !string.IsNullOrWhiteSpace(authorName) ? new Author(authorName) : CreateEmptyAuthor();

    public static Author CreateEmptyAuthor() => new(string.Empty);
}