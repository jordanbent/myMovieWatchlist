using System.Collections.Generic;
using System;

namespace myMovieWatchlistLibrary.Models
{
    public class Movie
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
