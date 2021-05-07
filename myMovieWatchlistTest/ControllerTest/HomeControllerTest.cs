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
    public class HomeControllerTest
    {

        private Mock<IRepositoryWrapper> mockRepo;
        private HomeController homeController;
        private List list;
        private List<List> lists;
        private Mock<IList> listMock;
        private List<IList> listsMock;

        //Arrange
        //Act
        //Assert

        [Fact]
        public void ConstructorTest()
        {
            listMock = new Mock<IList>();
            listsMock = new List<IList> { listMock.Object };

            list = new List();
            lists = new List<List>();

            mockRepo = new Mock<IRepositoryWrapper>();
            homeController = new HomeController(mockRepo.Object);
        }
        [Fact]
        public void CreateTest()
        {

            //Create - View
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            homeController = new HomeController(mockRepo.Object);

            //Act
            var actionResult = homeController.Create();

            //Assert
            Assert.NotNull(actionResult);

            //Create - List
            //Arrange
            list = new List(); list.ID = 1;  list.Name = "test"; list.Description = "tester";
            mockRepo = new Mock<IRepositoryWrapper>();
            homeController = new HomeController(mockRepo.Object);
            mockRepo.Setup(repo => repo.Lists.Create(It.IsAny<List>())).Returns(It.IsAny<List>());

            //Act
            actionResult = homeController.Create(list);
            
            //Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void IndexTest()
        {
            //Index - View
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            homeController = new HomeController(mockRepo.Object);
            mockRepo.Setup(repo => repo.Lists.FindAll()).Returns(It.IsAny<List<List>>());

            //Act
            var actionResult = homeController.Index();

            //Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void DetailsTest()
        {
            //Details - View
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            homeController = new HomeController(mockRepo.Object);

            //Act
            var actionResult = homeController.Details(1);

            //Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void UpdateTest()
        {
            //Update - View
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            homeController = new HomeController(mockRepo.Object);
            mockRepo.Setup(repo => (repo.Lists.FindByCondition(c => c.ID == It.IsAny<int>()))).Returns(GetLists());

            //Act
            var actionResult = homeController.Update(1);

            //Assert
            Assert.NotNull(actionResult);

            //Update - List
            //Arrange
            list = new List(); list.ID = 1; list.Name = "test"; list.Description = "tester";
            mockRepo = new Mock<IRepositoryWrapper>();
            homeController = new HomeController(mockRepo.Object);
            mockRepo.Setup(repo => (repo.Lists.FindByCondition(c => c.ID == It.IsAny<int>()))).Returns(GetLists());

            //Act
            actionResult = homeController.Update(list,1);

            //Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void DeleteTest()
        {
            //Delete - List
            //Arrange
            mockRepo = new Mock<IRepositoryWrapper>();
            homeController = new HomeController(mockRepo.Object);
            mockRepo.Setup(repo => (repo.Lists.FindByCondition(c => c.ID == It.IsAny<int>()))).Returns(GetLists());

            //Act
            var actionResult = homeController.Delete(1);

            //Assert
            Assert.NotNull(actionResult);
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
    }
}
