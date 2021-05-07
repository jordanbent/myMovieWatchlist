using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMovieWatchlistApp;
using myMovieWatchlistLibrary.Models;
using Moq;
using Xunit;
using myMovieWatchlistLibrary.Interfaces;
using myMovieWatchlistLibrary.Repositories;
using myMovieWatchlistApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace myMovieWatchlistTest.ControllerTest
{
    public class ListControllerTest
    {
        private Mock<IRepositoryWrapper> mockRepo;
        private ListController listController;
        private List list;
        private List<List> lists;
        private Mock<IList> listMock;
        private List<IList> listsMock;
        private Movie movie;
        private List<Movie> movies;
        private Mock<IMovie> movieMock;
        private List<IMovie> moviesMock;
        private MovieList movieList;
        private List<MovieList> movieLists;
        private Mock<IMovieList> movieListMock;
        private List<IMovieList> movieListsMock;

        [Fact]
        public void ConstructorTest()
        {
            listMock = new Mock<IList>();
            listsMock = new List<IList> { listMock.Object };
            Assert.NotNull(listMock);
            Assert.NotNull(listsMock);

            list = new List();
            lists = new List<List>();

            movieMock = new Mock<IMovie>();
            moviesMock = new List<IMovie> { movieMock.Object };
            Assert.NotNull(movieMock);
            Assert.NotNull(moviesMock);

            movie = new Movie();
            movies = new List<Movie>();

            movieListMock = new Mock<IMovieList>();
            movieListsMock = new List<IMovieList> { movieListMock.Object };
            Assert.NotNull(movieListMock);
            Assert.NotNull(movieListsMock);

            movieList = new MovieList();
            movieLists = new List<MovieList>();

            mockRepo = new Mock<IRepositoryWrapper>();
            listController = new ListController(mockRepo.Object);
        }
        [Fact]
        public void CreateTest()
        {
            //Create - View
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            listController = new ListController(mockRepo.Object);

            //Act
            var actionResult = listController.Create(1);

            //Assert
            Assert.NotNull(actionResult);

            //Create - Movie and MovieList
            //Arrange
            movie = new Movie(); movie.ID = 1; movie.Name = "test"; movie.Year = 1; movie.DateAdded = "1/2/3"; movie.Watched = false;
            mockRepo = new Mock<IRepositoryWrapper>();
            listController = new ListController(mockRepo.Object);
            mockRepo.Setup(repo => repo.Movies.Create(It.IsAny<Movie>())).Returns(It.IsAny<Movie>());
            mockRepo.Setup(repo => repo.MovieLists.Create(It.IsAny<MovieList>())).Returns(It.IsAny<MovieList>());

            //Act
            actionResult = listController.Create(movie, 1);

            //Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void DetailsTest()
        {
            //Details - Movie
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            listController = new ListController(mockRepo.Object);
            mockRepo.Setup(repo => (repo.Lists.FindByCondition(c => c.ID == It.IsAny<int>()))).Returns(GetLists());
            mockRepo.Setup(repo => repo.MovieLists.FindAll()).Returns(GetMovieLists());
            mockRepo.Setup(repo => repo.Movies.FindAll()).Returns(GetMovies());

            //Act
            var actionResult = listController.Details(1);

            //Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void UpdateTest()
        {
            //Update - View
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            listController = new ListController(mockRepo.Object);
            mockRepo.Setup(repo => (repo.Movies.FindByCondition(c => c.ID == It.IsAny<int>()))).Returns(GetMovies());

           //Act
            var actionResult = listController.Update(1, 1);

            //Assert
            Assert.NotNull(actionResult);

            //Update - Movie
            //Arrange
            movie = new Movie(); movie.ID = 1; movie.Name = "test"; movie.Year = 1; movie.DateAdded = "1/2/3"; movie.Watched = false;
            mockRepo = new Mock<IRepositoryWrapper>();
            listController = new ListController(mockRepo.Object);
            mockRepo.Setup(repo => (repo.Movies.FindByCondition(c => c.ID == It.IsAny<int>()))).Returns(GetMovies());

            //Act
            actionResult = listController.Update(movie, 1, 1);

            //Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void DeleteTest()
        {
            //Delete - List
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            listController = new ListController(mockRepo.Object);
            mockRepo.Setup(repo => (repo.Movies.FindByCondition(c => c.ID == It.IsAny<int>()))).Returns(GetMovies());
            mockRepo.Setup(repo => (repo.MovieLists.FindByCondition(c => ((c.MovieID == It.IsAny<int>())&&(c.ListID == It.IsAny<int>()))))).Returns(GetMovieLists());

            //Act
            var actionResult = listController.Delete(1, 1);

            //Assert
            Assert.NotNull(actionResult);
            return;
        }

        private IEnumerable<List> GetLists()
        {
            return new List<List>() {
                new List { ID = 1, Name = "test", Description = "tester"},
                new List { ID = 2, Name = "test", Description = "tester"},
            };
        }
        private List GetList()
        {
            return GetLists().ToList()[0];
        }
        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>() {
                new Movie { ID = 1, Name = "test", Year = 1, DateAdded = "1/2/3", Watched = false},
                new Movie { ID = 2, Name = "test", Year = 1, DateAdded = "1/2/3", Watched = false},
            };
        }
        private Movie GetMovie()
        {
            return GetMovies().ToList()[0];
        }
        private IEnumerable<MovieList> GetMovieLists()
        {
            var movielists = new List<MovieList> {
            new MovieList(){ID=1, MovieID=1, ListID=1},
            new MovieList(){ID=2, MovieID=2, ListID=1}
            };
            return movielists;
        }
        private MovieList GetMovieList()
        {
            return GetMovieLists().ToList()[0];
        }
    }
}
