using System;
using GMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GMS.Data
{
    public class DataContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Availability> Availabilities { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
            modelBuilder.Entity<Instrument>().ToTable("Instrument");
            modelBuilder.Entity<Availability>().ToTable("Availability");

            modelBuilder.Entity<Availability>()
                .HasKey(c => new { c.TeacherId, c.DateTime });

            modelBuilder.Entity<Lesson>()
                .HasKey(c => new { c.TeacherId, c.StudentId, c.DateTime });

        }
    }
}
