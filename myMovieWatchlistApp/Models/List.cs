using System;
using System.Collections.Generic;

namespace myMovieWatchlistApp.Models
{
    public class List
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
