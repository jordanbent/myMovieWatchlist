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
    public class ListController : ControllerBase
    {
        // Initialising Database
        private readonly ApplicationDbContext dbContext;

        // Controller Construstor
        // - assigning given database to local database variable
        public ListController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        // CREATE
        // -adding a new movie
        // --once new movie is clicked on the list page, Details, Create() returns the Create View to prompt user to add a movie.
        [Route("addmovie/{listID:int}")]
        public IActionResult Create(int listID)
        {
            ViewBag.ID = listID;
            return View();
        }

        // -after user has added a movie it is created, added to database.
        // --then a movie list item is created, this links the movie's parent list to the movie itself in a seperate database table.
        // ---the movielist is created and added to the database. Now the user can see the movie in their specified list.
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

        // READ
        // -reading and showing the contents of the watchlist, i.e. all movies in this list 
        // --the list item is found from the database using it's ID, this is to get all list data to display on the list page, stored in a ViewBag.
        // ---a list of movies is created, this list will be passed to the Describe view as all mvoies in this list of movies.
        // ----a list of all movies in the database is created/
        // -----the movielist and movies are cross compared to collect all movies that belong to this listID.
        [Route("details/{listID:int}")]
        public IActionResult Details(int listID)
        {
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

        // UPDATE
        // -when a user wants to update information of a movie
        // --the will be brought to the update view in list.
        [Route("updatemovie/{movieID:int}/{listID:int}")]
        public IActionResult Update(int movieID, int listID)
        {
            ViewBag.ID = listID;
            return View(dbContext.Movies.FirstOrDefault(c => c.ID == movieID));
        }

        // -returning from the update view, the movie is grabbed from the database and updated.
        // --the user is returned to the list page with the updated information visible.
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

        // DELETE
        // -when delete is pressed this function occurs
        // --the movie is found in the database and deleted and the user is given a refreshed list page without the movie
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
