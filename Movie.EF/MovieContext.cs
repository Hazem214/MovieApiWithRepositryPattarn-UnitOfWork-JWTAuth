using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movie.EF
{
    public class MovieContext:IdentityDbContext<User>
    {
        public MovieContext(DbContextOptions options) : base(options)
        {
            

        }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<MovieDetail> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            new GenreConfigrations().Configure(builder.Entity<Genre>());

            builder.SeedData();

            base.OnModelCreating(builder);

        }

    }
}
