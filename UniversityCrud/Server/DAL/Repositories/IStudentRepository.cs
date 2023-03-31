using System;
using System.Linq.Expressions;
using UniversityCrud.Shared.Model;

namespace UniversityCrud.Server.DAL.Repositories
{
    public interface IStudentRepository
    {

        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetById(int id);
        Task <IQueryable<Student>> FindByCondition(Expression<Func<Student, bool>> expression);
        Task Insert(Student student);
        Task Update(Student student);
        Task Delete(int id);
    }
}

