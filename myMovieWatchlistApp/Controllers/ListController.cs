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
        [Route("addmovie/{listID:int}")]
        public IActionResult Create(int listID)
        {
            ViewBag.ID = listID;
            return View();
        }
        [HttpPost]
        [Route("addmovie/{listID:int}")]
        public IActionResult Create(Movie movie, int listID)
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

            MovieList newML = new MovieList
            {
                ListID = listID,
                MovieID = newMovie.ID
            };
            dbContext.MovieList.Add(newML);
            dbContext.SaveChanges();
            return RedirectToAction("Details", new { listID });
        }
        // Read Functionailty
        public IActionResult Index()
        {
            return View();
        }
        [Route("details/{listID:int}")]
        public IActionResult Details(int listID)
        {
            var movieList = dbContext.MovieList.FirstOrDefault(c => c.ID == listID);
            var list = dbContext.Lists.FirstOrDefault(l => l.ID == listID);
            ViewBag.ID = listID;
            ViewBag.ListName = list.Name;
            ViewBag.ListDes = list.Description;
            List<Movie> movies = new List<Movie>();
            var allMovies = dbContext.Movies.ToList();
            foreach (MovieList ml in dbContext.MovieList)
            {
                if (ml.ListID == listID)
                {
                    foreach (var mov in allMovies)
                    {
                        if(mov.ID == ml.MovieID)
                            movies.Add(mov);
                    }
                }
            }
            ViewBag.Movies = movies;
            return View(movies);
        }

        // Update Functionality
        [Route("updatemovie/{movieID:int}/{listID:int}")]
        public IActionResult Update(int movieID, int listID)
        {
            ViewBag.ID = listID;
            return View(dbContext.Movies.FirstOrDefault(c => c.ID == movieID));
        }
        [HttpPost("updatemovie/{movieID:int}/{listID:int}")]
        public IActionResult Update(Movie movie, int movieID, int listID)
        {
            var updateMovie = dbContext.Movies.FirstOrDefault(c => c.ID == movieID);

            updateMovie.Name = movie.Name;
            updateMovie.Year = movie.Year;
            updateMovie.Watched = movie.Watched;

            dbContext.SaveChanges();
            return RedirectToAction("Details", new { listID });
        }
        // Delete Functionality
        [Route("deletemovie/{movieID:int}/{listID:int}")]
        public IActionResult Delete(int movieID, int listID)
        {
            var deleteMovie = dbContext.Movies.FirstOrDefault(c => c.ID == movieID);
            dbContext.Movies.Remove(deleteMovie);
            dbContext.SaveChanges();

            var deleteML = dbContext.MovieList.FirstOrDefault(c => (c.MovieID == movieID)&&(c.ListID == listID));
            dbContext.MovieList.Remove(deleteML);
            dbContext.SaveChanges();

            return RedirectToAction("Details", new { listID });
        }
    }
}
