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
    public class MovieListTest
    {
        [Fact]
        public void ConstructorTest()
        {
            //Null test
            MovieList testMovieList = new MovieList();
            Assert.NotNull(testMovieList);

            //ID test
            testMovieList.ID = 1;
            Assert.Equal(1, testMovieList.ID);

            //Name test
            testMovieList.ListID = 2;
            Assert.Equal(2, testMovieList.ListID);

            //Description test
            testMovieList.MovieID = 3;
            Assert.Equal(3, testMovieList.MovieID);
        }
    }
}
