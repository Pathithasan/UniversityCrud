using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityCrud.Server.DAL.Repositories;
using UniversityCrud.Shared.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityCrud.Server.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository context)
        {
            _courseRepository = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetAllCoursees()
        {
            var coursees = await _courseRepository.GetAll();
            return Ok(coursees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            var course = await _courseRepository.GetById(id);

            if (course == null)
            {
                return NotFound("Sorry, no course here. :/");
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<List<Course>>> CreateCourse(Course course)
        {
            var dbCourses = await _courseRepository.FindByCondition(s => s.Title.Equals(course.Title));
            if (dbCourses.Count() == 0)
            {
                course.Id = 0;
                course.DateCreated = DateTime.Now;
                course.DateUpdated = DateTime.Now;
                await _courseRepository.Insert(course);
                return Ok(await _courseRepository.GetAll());
            }
            else
            {
                return NotFound("Sorry, Given email is already existing. :/");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Course>>> UpdateCourse(Course course, int id)
        {
            var dbCourse = await _courseRepository.GetById(id);
            if (dbCourse != null)
            {
                dbCourse.Description = course.Description;
                dbCourse.DateUpdated = DateTime.Now;

                if (dbCourse.Title == course.Title)
                {
                    await _courseRepository.Update(course);

                    return Ok(await _courseRepository.GetAll());
                }
                else
                {
                    var dbCourses = await _courseRepository.FindByCondition(s => s.Title.Equals(course.Title));
                    if (dbCourses.Count() == 0)
                    {
                        dbCourse.Title = course.Title;

                        await _courseRepository.Update(course);

                        return Ok(await _courseRepository.GetAll());
                    }
                    else
                    {
                        return NotFound("Sorry, Given title is already existing. :/");
                    }
                }
            }
            else
            {
                return NotFound("Sorry, but no course for given ID. :/");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Course>>> DeleteCourse(int id)
        {
            var dbCourse = await _courseRepository.GetById(id);

            if (dbCourse == null)
            {
                return NotFound("Sorry, but no course for you. :/");
            }
            else
            {
                await _courseRepository.Delete(id);
                return Ok(await _courseRepository.GetAll());

            }
        }
    }
}

