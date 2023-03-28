using System;
using UniversityCrud.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace UniversityCrud.Server.DAL
{
    public class UniversityDBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "Information Technology",
                    Description = "Department of Information Technology",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    IsActive = true
                },
                new Course
                {
                    Id = 2,
                    Title = "Information Technology and Management",
                    Description = "Department of Information Technology and Management",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    IsActive = true
                });

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 100000,
                    FirstName = "Pathithasan",
                    LastName = "Rajakopal",
                    Email = "rpathithasan0@gmail.com",
                    DateOfBirth = new DateTime(1992,1,5),
                    CourseId = 1,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    IsActive = true
                });

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Title)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique(true);
        }


    }
}

