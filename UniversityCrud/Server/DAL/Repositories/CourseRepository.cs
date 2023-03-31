using System;
using System.Collections.Generic;
using UniversityCrud.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniversityCrud.Server.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private UniversityDBContext _context;
        private DbSet<Course> table;
        public CourseRepository(UniversityDBContext context)
        {
            this._context = context;
            this.table = context.Set<Course>();
        }

        /// <summary>
        /// Delete the course by id 
        /// </summary>
        /// <param name="id">The Id of course to delete</param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            Course? existing = table.Find(id);
            if (existing != null)
            {
                table.Remove(existing);
                await _context.SaveChangesAsync();
            }

        }

        /// <summary>
        /// Get a collection of courses by condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>Selected courses</returns>
        public async Task<IQueryable<Course>> FindByCondition(Expression<Func<Course, bool>> expression)
        {
            return await Task.FromResult(table.Where(expression).AsQueryable().AsNoTracking());
        }

        /// <summary>
        /// Get a collection of all courses
        /// </summary>
        /// <returns>A return of all courses</returns>
        public async Task<IEnumerable<Course>> GetAll()
        {
            return await table.ToListAsync();
        }

        /// <summary>
        /// Gets an course by ID
        /// </summary>
        /// <param name="id"> The ID of course to retrive</param>
        /// <returns>The course object if found, otherwise null</returns>
        public async Task<Course> GetById(int id)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await table.FindAsync(id);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        /// <summary>
        /// Add an course
        /// </summary>
        /// <param name="course">The course to add</param>
        /// <returns></returns>
        public async Task Insert(Course course)
        {
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Modify the course, If exist
        /// </summary>
        /// <param name="course">The course to modify</param>
        /// <returns></returns>
        public async Task Update(Course course)
        {
            table.Attach(course);
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

