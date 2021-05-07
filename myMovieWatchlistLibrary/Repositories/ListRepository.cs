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
    public class ListRepository : Repository<List>, IListRepository
    {
        public ListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
