using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMovieWatchlistLibrary.Models;
using myMovieWatchlistLibrary.Repositories;

namespace myMovieWatchlistLibrary.Interfaces
{
    public interface IRepositoryWrapper
    {
        IListRepository Lists { get; }
        IMovieRepository Movies { get; }
        IMovieListRepository MovieLists { get; }
        void Save();
    }
}
