namespace MovieStore;
public sealed class StoreAccount
{
    public List<MovieSale> AllSales { get; set; }

    public double TotalSold { get; set; } = 0d;

    public StoreAccount()
    {
        AllSales = new List<MovieSale>();
    }

    public void Sell(Movie movie, string to)
    {
        TotalSold += movie.UnitPrice;
        AllSales.Add(new MovieSale(to, movie.Title));
    }

    public sealed class MovieSale
    {
        public string ClientName { get; set; }

        public string MovieName { get; set; }

        public MovieSale(string clientName, string movieName)
        {
            ClientName = clientName;
            MovieName = movieName;
        }
    }
}