using System;
using System.Linq.Expressions;
using UniversityCrud.Shared.Model;

namespace UniversityCrud.Server.DAL.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(int id);
        Task<IQueryable<Course>> FindByCondition(Expression<Func<Course, bool>> expression);
        Task Insert(Course course);
        Task Update(Course course);
        Task Delete(int id);
    }
}

