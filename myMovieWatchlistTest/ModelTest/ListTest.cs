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
    public class ListTest
    {
        [Fact]
        public void ConstructorTest()
        {
            //Null test
            List testList = new List();
            Assert.NotNull(testList);

            //ID test
            testList.ID = 1;
            Assert.Equal(1, testList.ID);

            //Name test
            testList.Name = "test";
            Assert.Equal("test", testList.Name);

            //Description test
            testList.Description = "test list";
            Assert.Equal("test list", testList.Description);
        }
    }
}
