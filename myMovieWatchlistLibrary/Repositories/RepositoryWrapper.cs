using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMovieWatchlistLibrary.Models;
using myMovieWatchlistLibrary.Interfaces;
using myMovieWatchlistLibrary.Data;
using Microsoft.Extensions.Caching.Memory;

namespace myMovieWatchlistLibrary.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        ApplicationDbContext _repoContext;
        public RepositoryWrapper(ApplicationDbContext repoContext)
        {
            _repoContext = repoContext;
        }
        IListRepository _lists;
        public IListRepository Lists
        {
            get 
            {
                if (_lists == null)
                    _lists = new ListRepository(_repoContext);
                return _lists;
            }
        }

        IMovieRepository _movies;
        public IMovieRepository Movies
        {
            get
            {
                if (_movies == null)
                    _movies = new MovieRepository(_repoContext);
                return _movies;
            }
        }

        IMovieListRepository _movielists;
        public IMovieListRepository MovieLists
        {
            get
            {
                if (_movielists == null)
                    _movielists = new MovieListRepository(_repoContext);
                return _movielists;
            }
        }
        void IRepositoryWrapper.Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
