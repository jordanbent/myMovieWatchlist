using myMovieWatchlistLibrary.Models;
using myMovieWatchlistLibrary.Interfaces;
using myMovieWatchlistLibrary.Repositories;
using Microsoft.Extensions.Logging;
using myMovieWatchlistApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myMovieWatchlistLibrary.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System;

namespace myMovieWatchlistApp.Controllers
{
    public class HomeController : Controller
    {
        // Initialising Database
        //private readonly ApplicationDbContext dbContext;
        private IRepositoryWrapper _repo;

        // Controller Construstor
        // - assigning given database to local database variable
        //public HomeController(ApplicationDbContext applicationDbContext)
        //{
        //    dbContext = applicationDbContext;
        //}
        public HomeController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // CREATE
        // -making a new list
        // --once new list is clicked on the homepage, Index, Create() returns the Create View to prompt user to add a list.
        [Route("addlist")]
        public IActionResult Create()
        {
            return View();
        }

        // -after user has added a list it is created, added to database and shown to the user through the userface.
        [HttpPost("addlist")]
        public IActionResult Create(List list)
        {
            List newlist = new List
            {
                Name = list.Name,
                Description = list.Description
            };
            //dbContext.Lists.Add(newlist);
            _repo.Lists.Create(newlist);
            //dbContext.SaveChanges();
            _repo.Save();
            return RedirectToAction("Index");
        }

        // READ
        // -reading and showing all lists, in a list format
        [Route("")]
        public IActionResult Index()
        {
            //return View(dbContext.Lists.ToList());
            return View(_repo.Lists.FindAll());
        }

        // -launched when a user clicks a list name.
        // --this will move you to the details view found in the list controller, with the list to view given.
        [Route("details/{id:int}")]
        public IActionResult Details(int id)
        {
            return RedirectToAction("ListController/Details");
        }

        // UPDATE
        // -when a user wants to update information of a list
        // --the will be brought to the update view in home.
        [Route("updatelist/{id:int}")]
        public IActionResult Update(int id)
        {
            //return View(dbContext.Lists.FirstOrDefault(c => c.ID == id));
            return View(_repo.Lists.FindByCondition(c => c.ID == id).FirstOrDefault());
        }

        // -returning from the update view, the list is grabbed from the database and updated.
        // --the user is returned to the home page with the updated information visible.
        [HttpPost("updatelist/{id:int}")]
        public IActionResult Update(List list, int id)
        {
            //var updateList = dbContext.Lists.FirstOrDefault(c => c.ID == id);
            var updateList = _repo.Lists.FindByCondition(c => c.ID == id).FirstOrDefault();

            updateList.Name = list.Name;
            updateList.Description = list.Description;

            //dbContext.SaveChanges();#
            _repo.Save();
            return RedirectToAction("Index");
        }

        // DELETE
        // -when delete is pressed this function occurs
        // --the list is found in the database and deleted and the user is given a refreshed page without the list
        [Route("deletelist/{id:int}")]
        public IActionResult Delete(int id)
        {
            //var deleteList = dbContext.Lists.FirstOrDefault(c => c.ID == id);
            var deleteList = _repo.Lists.FindByCondition(c => c.ID == id).FirstOrDefault();

            //dbContext.Lists.Remove(deleteList);
            _repo.Lists.Delete(deleteList);
            //dbContext.SaveChanges();
            _repo.Save();
            return RedirectToAction("Index");
        }
    }
}
