using System;
using GMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GMS.Data
{
    public class DataContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        /// <summary>
        /// A call to the super constructor
        /// </summary>
        /// <param name="options">Options for Database Model such as the Connection String</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Availability> Availabilities { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Instrument> Instruments { get; set; }


        /// <summary>
        /// Executed when EF is being initialised by the application
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Models are mapped to singular names. i.e. Student is mapped to a table named Student instead of Students
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
            modelBuilder.Entity<Instrument>().ToTable("Instrument");
            modelBuilder.Entity<Availability>().ToTable("Availability");

            // Composite key for Availability
            modelBuilder.Entity<Availability>()
                .HasKey(c => new { c.UserId, c.DateTime });

            // Composite key for Lesson
            modelBuilder.Entity<Lesson>()
                .HasKey(c => new { c.TaughtById, c.TaughtToId, c.DateTime });

            modelBuilder.Entity<InstumentType>().HasKey(i => new {i.Type, i.UserId});


        }
     
    }
}
