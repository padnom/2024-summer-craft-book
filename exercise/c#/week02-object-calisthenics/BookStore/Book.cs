namespace BookStore;
public sealed class Book
{
    public Author Author { get; }
    public Copies Copies { get; private set; }
    public bool HasCopies => Copies.Value > 0;
    public bool IsValid => Title.IsValid && Author.IsValid && Copies.IsValid;
    public Title Title { get; }

    private Book(Title title, Author author, Copies copies)
    {
        Title = title;
        Author = author;
        Copies = copies;
    }

    public void AddCopies(int additionalCopies)
    {
        Copies = Copies.AddCopies(additionalCopies);
    }

    public static Book? CreateBook(Title title, Author author, Copies copies) =>
        title.IsValid && author.IsValid && copies.IsValid ? new Book(title, author, copies) : CreateInvalidBook();

    public void RemoveCopies(int soldCopies)
    {
        Copies = Copies.RemoveCopies(soldCopies);
    }

    private static Book? CreateInvalidBook() => new(Title.CreateEmptyTitle(), Author.CreateEmptyAuthor(), Copies.CreateEmptyCopies());
}