using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myMovieWatchlistApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using myMovieWatchlistApp.Data;

namespace myMovieWatchlistApp.Controllers
{
    public class HomeController : Controller
    {
        // Initialising Database
        private readonly ApplicationDbContext dbContext;

        // Controller Construstor
        // - assigning given database to local database variable
        public HomeController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        // Create Functionaility
        // Making a new list
        [Route("addlist")]
        public IActionResult Create()
        {
            return View();
        }
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

        // Read Functionailty
        // Reading all lists
        [Route("")]
        public IActionResult Index()
        {
            return View(dbContext.Lists.ToList());
        }
        [Route("details/{id:int}")]
        public IActionResult Details(int id)
        {
            return RedirectToAction("ListController/Details");
        }
        // Update Functionality
        [Route("updatelist/{id:int}")]
        public IActionResult Update(int id)
        {
            return View(dbContext.Lists.FirstOrDefault(c=>c.ID==id));
        }
        [HttpPost("updatelist/{id:int}")]
        public IActionResult Update(List list, int id)
        {
            var updateList = dbContext.Lists.FirstOrDefault(c => c.ID == id);

            updateList.Name = list.Name;
            updateList.Description = list.Description;
   
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        // Delete Functionality
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
