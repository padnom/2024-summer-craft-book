﻿using Xunit;

namespace MovieStore.Tests
{
    public class MovieStoreTest
    {
        private readonly MovieStore _store = new();

        public MovieStoreTest() => SetUp();

        //Tests interdependency: the setup assumes a set of movies for all tests which makes them prone to fail.
        private void SetUp()
        {
            _store.AddMovie("001", "Inception", "Christopher Nolan", 10, 0d);
            _store.AddMovie("002", "The Matrix", "Lana Wachowski, Lilly Wachowski", 8, 0d);
            _store.AddMovie("003", "Dunkirk", "Christopher Nolan", 5, 0d);
        }

        [Fact]
        public void TestAddMovie()
        {
            _store.AddMovie("002", "The Matrix", "Lana Wachowski, Lilly Wachowski", 8, 0d);
            //Weak assertions: Checks for not null but not for correct properties
            Assert.NotNull(_store.AllMovies["002"]);
            //Implementation leak: tight to the implementation details of MovieStore
            Assert.Equal(8, _store.AllMovies["002"].TotalCopies);
        }

        [Fact]
        public void TestRemoveMovie()
        {
            _store.RemoveMovie("001");
            Assert.False(_store.AllMovies.ContainsKey("001"));
        }

        [Fact]
        public void TestBorrowMovie()
        {
            _store.BorrowMovie("001");
            Assert.Equal(1, _store.AllMovies["001"].BorrowedCopies);
        }

        [Fact]
        public void TestBuyMovie()
        {
            var movie = _store.AllMovies["001"];
            //Mutable state between tests: changing global set for a specific state
            movie.UnitPrice = 5d;

            _store.BuyMovie("Durant", "001");

            Assert.Equal(9, _store.AllMovies["001"].TotalCopies);
        }

        [Fact]
        public void TestReturnMovie()
        {
            _store.ReturnMovie("001");
            Assert.Equal(0, _store.AllMovies["001"].BorrowedCopies);
        }

        [Fact]
        public void TestFindMoviesByTitle()
        {
            var movies = _store.FindMoviesByTitle("Inception");
            Assert.Single(movies);
            Assert.Equal("Inception", movies[0].Title);
        }
    }
}