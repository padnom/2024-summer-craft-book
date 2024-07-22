namespace BookStore;
public class BookStore
{
    private readonly BookInventory _bookInventory = [];

    public void AddBook(Title title, Author author, Copies copies)
    {
        var book = GetBook(title, author);

        if (book != null)
        {
            book.AddCopies(copies.Value);

            return;
        }

        var newBook = Book.CreateBook(title, author, copies);

        _bookInventory.Add(newBook);
    }

    public void SellBook(Title title, Author author, Copies copies)
    {
        var book = GetBook(title, author);

        if (book == null)
        {
            return;
        }

        book.RemoveCopies(copies.Value);
        _bookInventory.RemoveBookIfNoMoreCopies(book);
    }

    protected Book? GetBook(Title title, Author author)
    {
        return _bookInventory.Find(b => b.Title.Value == title.Value && b.Author.Value == author.Value);
    }
}