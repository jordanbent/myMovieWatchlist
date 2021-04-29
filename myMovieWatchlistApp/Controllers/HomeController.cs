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
        [Route("addlist")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("addlist")]
        public IActionResult Create(List list)
        {
            var newlist = new List
            {
                Name = list.Name,
                Description = list.Description
            };
            dbContext.Lists.Add(newlist);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        // Read Functionailty
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
        [Route("updatemovie/{id:int}")]
        public IActionResult Update(int id)
        {
            return View(dbContext.Movies.FirstOrDefault(c=>c.ID==id));
        }
        [HttpPost("updatemovie/{id:int}")]
        public IActionResult Update(Movie movie, int id)
        {
            var updateMovie = dbContext.Movies.FirstOrDefault(c => c.ID == id);
            
            updateMovie.Name = movie.Name;
            updateMovie.Year = movie.Year;
            updateMovie.Watched = movie.Watched;
   
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        // Delete Functionality
        [Route("deletemovie/{id:int}")]
        public IActionResult Delete(int id)
        {
            var deleteMovie = dbContext.Movies.FirstOrDefault(c => c.ID == id);
           
            dbContext.Movies.Remove(deleteMovie);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
