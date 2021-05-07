using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMovieWatchlistLibrary.Interfaces
{
    public interface IMovieList
    {
        public int ID { get; set; }
        public int ListID { get; set; }
        public int MovieID { get; set; }
    }
}
