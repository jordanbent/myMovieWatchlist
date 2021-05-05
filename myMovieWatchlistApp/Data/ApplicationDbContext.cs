using Microsoft.EntityFrameworkCore;
using myMovieWatchlistLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace myMovieWatchlistApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<MovieList> MovieList { get; set; }
    }
}