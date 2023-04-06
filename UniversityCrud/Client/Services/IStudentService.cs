using UniversityCrud.Shared.Model;

namespace UniversityCrud.Client.Services
{
	public interface IStudentService
	{
        List<Student> Students { get; set; }
        Task GetStudents();
        Task<Student> GetStudent(int id);
        Task CreateStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int id);
    }
}

