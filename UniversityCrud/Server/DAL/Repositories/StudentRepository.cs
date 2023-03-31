using System;
using System.Collections.Generic;
using UniversityCrud.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UniversityCrud.Server.DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private UniversityDBContext _context;
        private DbSet<Student> table;
        public StudentRepository(UniversityDBContext context)
        {
            this._context = context;
            this.table = context.Set<Student>();
        }

        /// <summary>
        /// Delete the student by id 
        /// </summary>
        /// <param name="id">The Id of student to delete</param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            Student? existing = table.Find(id);
            if (existing != null)
            {
                table.Remove(existing);
                await _context.SaveChangesAsync();
            }

        }

        /// <summary>
        /// Get a collection of students by condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>Selected students</returns>
        public async Task<IQueryable<Student>> FindByCondition(Expression<Func<Student, bool>> expression)
        {
            return await Task.FromResult(table.Where(expression).AsQueryable().AsNoTracking()); 
        }

        /// <summary>
        /// Get a collection of all students
        /// </summary>
        /// <returns>A return of all students</returns>
        public async Task<IEnumerable<Student>> GetAll()
        {
            return await table.Include(s => s.Course).ToListAsync();
        }

        /// <summary>
        /// Gets an student by ID
        /// </summary>
        /// <param name="id"> The ID of student to retrive</param>
        /// <returns>The student object if found, otherwise null</returns>
        public async Task<Student> GetById(int id)
        {
            
            #pragma warning disable CS8603 // Possible null reference return.
            return await table
                        .Include(s => s.Course).AsNoTracking()
                        .FirstOrDefaultAsync(h => h.Id == id);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        /// <summary>
        /// Add an student
        /// </summary>
        /// <param name="student">The student to add</param>
        /// <returns></returns>
        public async Task Insert(Student student)
        {
            student.Course = null;
            student.Id = 0;
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Modify the student, If exist
        /// </summary>
        /// <param name="student">The student to modify</param>
        /// <returns></returns>
        public async Task Update(Student student)
        {
            //table.Update(student);
            table.Attach(student);
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

