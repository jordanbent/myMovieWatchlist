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
    [Route("[Controller]/[Action]")]
    public class ListController : Controller
    {
        // Initialising Database
        private readonly ApplicationDbContext dbContext;

        // Controller Construstor
        // - assigning given database to local database variable
        public ListController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        // Create Functionaility
        [Route("addmovie")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("addmovie")]
        public IActionResult Create(Movie movie)
        {
            var newMovie = new Movie
            {
                Name = movie.Name,
                Year = movie.Year,
                DateAdded = DateTime.Now.Date,
                Watched = false
            };
            dbContext.Movies.Add(newMovie);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        // Read Functionailty
        [Route("details/{id:int}")]
        public IActionResult Details(int id)
        {
            var list = dbContext.Lists.FirstOrDefault(c => c.ID == id);
            return View(list);
        }
        // Update Functionality
        [Route("updatemovie/{id:int}")]
        public IActionResult Update(int id)
        {
            return View(dbContext.Movies.FirstOrDefault(c => c.ID == id));
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
