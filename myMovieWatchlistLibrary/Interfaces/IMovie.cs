using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMovieWatchlistLibrary.Interfaces
{
    public interface IMovie
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string DateAdded { get; set; }
        public Boolean Watched { get; set; }
        public string IsWatched
        {
            get
            {
                return (bool)this.Watched ? "Watched" : "To Watch";
            }
        }
    }
}
