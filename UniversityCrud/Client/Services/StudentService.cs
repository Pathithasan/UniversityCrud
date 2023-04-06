using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using UniversityCrud.Shared.Model;
using static System.Net.WebRequestMethods;

namespace UniversityCrud.Client.Services
{
	public class StudentService : IStudentService
	{
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public List<Student> Students { get; set; } = new List<Student>();

        public StudentService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task CreateStudent(Student student)
        {
            var result = await _http.PostAsJsonAsync("http://localhost:11806/Student/CreateStudent", student);
            await SetStudent(result);
        }

        public async Task DeleteStudent(int id)
        {
            var result = await _http.DeleteAsync($"http://localhost:11806/Student/DeleteStudent/{id}");
            await SetStudent(result);
        }

        public async Task<Student> GetStudent(int id)
        {
            var result = await _http.GetFromJsonAsync<Student>($"http://localhost:11806/Student/GetStudentById/{id}");
            if (result != null)
                return result;
            throw new Exception("Student not found!");
        }

        public async Task GetStudents()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("Student/GetAllStudentes");
            if (result != null)
                Students = result;
        }

        public async Task UpdateStudent(Student student)
        {
            var result = await _http.PutAsJsonAsync($"http://localhost:11806/Student/UpdateStudent/{student.Id}", student);
            await SetStudent(result);
        }

        private async Task SetStudent(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Student>>();
            Students = response;
            _navigationManager.NavigateTo("students");
        }
    }
}

