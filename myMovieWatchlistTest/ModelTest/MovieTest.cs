using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMovieWatchlistApp;
using myMovieWatchlistLibrary.Models;
using Xunit;

namespace myMovieWatchlistTest.ModelTest
{
    public class MovieTest
    {
        [Fact]
        public void ConstructorTest()
        {
            //Null test
            Movie testMovie = new Movie();
            Assert.NotNull(testMovie);

            //ID test
            testMovie.ID = 1;
            Assert.Equal(1, testMovie.ID);

            //Name test
            testMovie.Name = "test";
            Assert.Equal("test", testMovie.Name);

            //Year test
            testMovie.Year = 12345;
            Assert.Equal(12345, testMovie.Year);

            //DateAdded test
            testMovie.DateAdded = "03/04/05";
            Assert.Equal("03/04/05", testMovie.DateAdded);

            //Watched test
            testMovie.Watched = false;
            Assert.False(testMovie.Watched);
            testMovie.Watched = true;
            Assert.True(testMovie.Watched);
        }
        [Fact]
        public void IsWatchedTest()
        {
            Movie testMovie = new Movie
            {
                ID = 1,
                Name = "test",
                Year = 12345,
                DateAdded = "03/04/05",
                Watched = false
            };

            testMovie.Watched = false;
            Assert.Equal("To Watch", testMovie.IsWatched);
            testMovie.Watched = true;
            Assert.Equal("Watched", testMovie.IsWatched);
        }
    }
}
