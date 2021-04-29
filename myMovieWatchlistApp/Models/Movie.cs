using System;

namespace myMovieWatchlistApp.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public DateTime? DateAdded { get; set; }
        public Boolean Watched { get; set; }
        public virtual List List { get; set; }
    }
}
