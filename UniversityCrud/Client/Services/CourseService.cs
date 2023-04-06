using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using UniversityCrud.Shared.Model;
using static System.Net.WebRequestMethods;

namespace UniversityCrud.Client.Services
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public List<Course> Courses { get; set; } = new List<Course>();

        public CourseService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task CreateCourse(Course course)
        {
            var result = await _http.PostAsJsonAsync("http://localhost:11806/Course/CreateCourse", course);
            await SetCourse(result);
        }

        public async Task DeleteCourse(int id)
        {
            var result = await _http.DeleteAsync($"http://localhost:11806/Course/DeleteCourse/{id}");
            await SetCourse(result);
        }

        public async Task<Course> GetCourse(int id)
        {
            var result = await _http.GetFromJsonAsync<Course>($"http://localhost:11806/Course/GetCourseById/{id}");
            if (result != null)
                return result;
            throw new Exception("Course not found!");
        }

        public async Task GetCourses()
        {
            var result = await _http.GetFromJsonAsync<List<Course>>("Course/GetAllCoursees");
            if (result != null)
                Courses = result;
        }

        public async Task UpdateCourse(Course course)
        {
            var result = await _http.PutAsJsonAsync($"http://localhost:11806/Course/UpdateCourse/{course.Id}", course);
            await SetCourse(result);
        }

        private async Task SetCourse(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Course>>();
            Courses = response;
            _navigationManager.NavigateTo("courses");
        }
    }
}

