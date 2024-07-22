using Xunit;

namespace BookStore.Tests;
public class BookStoreTest
{
    private readonly BookStoreUnderTest _store = new();

    [Fact]
    public void AddANewBookToStore()
    {
        var bookToAdd = CreateABook(2);
        _store.AddBook(bookToAdd!.Title, bookToAdd.Author, bookToAdd.Copies);

        Assert.True(_store.VerifyBookIsInStore(bookToAdd), "The book should be in the inventory");
    }

    [Fact]
    public void AddAnExistingBookToStoreIncreasesTheCopies()
    {
        const int initialCopies = 2;
        const int additionalCopies = 3;
        var bookAdded = AddBookToStore(initialCopies);
        int expectedNumberOfCopy = additionalCopies + bookAdded.Copies.Value;

        _store.AddBook(bookAdded.Title, bookAdded.Author, Copies.Create(additionalCopies));

        Assert.True(_store.VerifyNumberOfCopyForBook(bookAdded, expectedNumberOfCopy),
                    $"The book should have the following number of copies {expectedNumberOfCopy}");
    }

    [Fact]
    public void CannotSellZeroCopyOfABook()
    {
        const int copiesSold = 0;
        const int initialCopies = 2;
        var bookToSell = AddBookToStore(initialCopies);
        var expectedNumberOfCopy = bookToSell.Copies;

        _store.SellBook(bookToSell.Title, bookToSell.Author, Copies.Create(copiesSold));

        Assert.True(_store.VerifyNumberOfCopyForBook(bookToSell, expectedNumberOfCopy.Value),
                    $"The book should have the following number of copies {expectedNumberOfCopy}");
    }

    [Fact]
    public void DoesNotAddBookToStoreIfInvalidAuthor()
    {
        var title = Title.CreateTitle("Lord of the ring");
        var invalideAuthor = Author.CreateEmptyAuthor();
        _store.AddBook(title, invalideAuthor, Copies.Create(2));
        Assert.False(_store.BookInInventory(title, invalideAuthor), "The book should not be in the inventory");
    }

    [Fact]
    public void DoesNotAddBookToStoreIfInvalidTitle()
    {
        var invalidTitle = Title.CreateEmptyTitle();
        var author = Author.CreateAuthor("JR Tolkien");
        var copies = Copies.Create(2);
        _store.AddBook(invalidTitle, author, copies);

        Assert.False(_store.BookInInventory(invalidTitle, author), "The book should not be in the inventory");
    }

    [Fact]
    public void DoesNotAddBookToStoreIfNoCopy()
    {
        var noCopy = Copies.Create(0);
        var bookToAdd = CreateABook(noCopy.Value);

        _store.AddBook(bookToAdd!.Title, bookToAdd.Author, noCopy);

        Assert.False(_store.VerifyBookIsInStore(bookToAdd), "The book should not be in the inventory");
    }

    [Fact]
    public void SellABookFromTheStore()
    {
        const int copiesSold = 1;
        const int initialCopies = 2;
        var bookToSell = AddBookToStore(initialCopies);
        int expectedNumberOfCopy = bookToSell.Copies.Value - copiesSold;

        _store.SellBook(bookToSell.Title, bookToSell.Author, Copies.Create(copiesSold));

        Assert.True(_store.VerifyNumberOfCopyForBook(bookToSell, expectedNumberOfCopy),
                    $"The book should have the following number of copies {expectedNumberOfCopy}");
    }

    [Fact]
    public void SellTheLastCopyOfABookFromTheStore()
    {
        const int copiesSold = 1;
        var bookToSell = AddBookToStore(copiesSold);

        _store.SellBook(bookToSell.Title, bookToSell.Author, Copies.Create(1));

        Assert.False(_store.VerifyBookIsInStore(bookToSell), "The book should no longer be in the inventory");
    }

    private Book AddBookToStore(int initialCopies)
    {
        var createdBook = CreateABook(initialCopies);
        _store.AddBook(createdBook!.Title, createdBook.Author, Copies.Create(initialCopies));

        return createdBook;
    }

    private static Book? CreateABook(int copies) =>
        Book.CreateBook(Title.CreateTitle("Lord of the ring"), Author.CreateAuthor("JR Tolkien"), Copies.Create(copies));

    private class BookStoreUnderTest : BookStore
    {
        public bool BookInInventory(Title title, Author author) => GetBook(title, author) != null;

        public bool VerifyBookIsInStore(Book? book) => book != null && BookInInventory(book);

        public bool VerifyNumberOfCopyForBook(Book? bookAdded, int expectedNumberOfCopy) =>
            VerifyBookIsInStore(bookAdded) && GetBook(bookAdded!.Title, bookAdded.Author).Copies.Value == expectedNumberOfCopy;

        private bool BookInInventory(Book bookSearched) => GetBook(bookSearched.Title, bookSearched.Author) != null;
    }
}