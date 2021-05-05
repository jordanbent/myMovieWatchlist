using myMovieWatchlistLibrary.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myMovieWatchlistAPI.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System;

namespace myMovieWatchlistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // Initialising Database
        private readonly ApplicationDbContext dbContext;

        // Controller Construstor
        // - assigning given database to local database variable
        public HomeController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
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
            dbContext.Lists.Add(newlist);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // READ
        // -reading and showing all lists, in a list format
        [Route("")]
        public IActionResult Index()
        {
            return View(dbContext.Lists.ToList());
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
            return View(dbContext.Lists.FirstOrDefault(c=>c.ID==id));
        }

        // -returning from the update view, the list is grabbed from the database and updated.
        // --the user is returned to the home page with the updated information visible.
        [HttpPost("updatelist/{id:int}")]
        public IActionResult Update(List list, int id)
        {
            var updateList = dbContext.Lists.FirstOrDefault(c => c.ID == id);

            updateList.Name = list.Name;
            updateList.Description = list.Description;
   
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE
        // -when delete is pressed this function occurs
        // --the list is found in the database and deleted and the user is given a refreshed page without the list
        [Route("deletelist/{id:int}")]
        public IActionResult Delete(int id)
        {
            var deleteList = dbContext.Lists.FirstOrDefault(c => c.ID == id);
           
            dbContext.Lists.Remove(deleteList);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
