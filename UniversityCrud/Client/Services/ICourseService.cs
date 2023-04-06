using System;
namespace UniversityCrud.Client.Services
{
	public interface ICourseService
	{
        List<Course> Courses { get; set; }
        Task GetCourses();
        Task<Course> GetCourse(int id);
        Task CreateCourse(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(int id);
    }
}

