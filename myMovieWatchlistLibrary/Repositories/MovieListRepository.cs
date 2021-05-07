using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMovieWatchlistLibrary.Models;
using myMovieWatchlistLibrary.Interfaces;
using myMovieWatchlistLibrary.Data;

namespace myMovieWatchlistLibrary.Repositories
{
    public class MovieListRepository : Repository<MovieList>, IMovieListRepository
    {
        public MovieListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
