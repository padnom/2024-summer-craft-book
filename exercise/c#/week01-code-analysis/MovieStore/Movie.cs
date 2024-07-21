namespace MovieStore;
public sealed class Movie(string movieId, string title, string director, int totalCopies, double unitPrice)
{
    public int BorrowedCopies { get; set; } = 0;

    public string Director { get; set; } = director;

    public string MovieId { get; set; } = movieId;

    public string Title { get; set; } = title;

    public int TotalCopies { get; set; } = totalCopies;

    public double UnitPrice { get; set; } = unitPrice;

    public bool CanSell()
    {
        return UnitPrice != 0d;
    }
}