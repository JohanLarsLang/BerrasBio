using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Models;


namespace BerrasBio.Data
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Lounge> Lounges { get; set; }
        public DbSet<Showing> Showings { get; set; }
        public DbSet<ShowingSeat> ShowingSeats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Lounge>().ToTable("Lounge");
            modelBuilder.Entity<Showing>().ToTable("Showing");
            modelBuilder.Entity<ShowingSeat>().ToTable("ShowingSeat");
        }

    }
}
