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
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository context)
        {
            _studentRepository = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudentes()
        {
            var studentes = await _studentRepository.GetAll();
            return Ok(studentes);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _studentRepository.GetById(id);
            //var student = await Task.FromResult(_studentRepository.FindByCondition(s => s.Id.Equals(id)).Result.FirstOrDefault());
            if (student == null)
            {
                return NotFound("Sorry, no student here. :/");
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> CreateStudent(Student student)
        {
            var dbStudents = await _studentRepository.FindByCondition(s => s.Email.Equals(student.Email));
            if (dbStudents.Count() == 0)
            {
                student.Course = null;
                student.Id = 0;
                student.DateCreated = DateTime.Now;
                student.DateUpdated = DateTime.Now;
                await _studentRepository.Insert(student);
                return Ok(await _studentRepository.GetAll());
            }
            else
            {
                return NotFound("Sorry, Given email is already existing. :/");
            }
               
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student student, int id)
        {
            var dbStudent = await _studentRepository.GetById(id);
            //var dbStudent = await Task.FromResult(_studentRepository.FindByCondition(s => s.Id.Equals(id)).Result.FirstOrDefault());
            if (dbStudent != null)
            {
                dbStudent.FirstName = student.FirstName;
                dbStudent.LastName = student.LastName;
                dbStudent.CourseId = student.CourseId;
                dbStudent.DateOfBirth = student.DateOfBirth;
                dbStudent.DateUpdated = DateTime.Now;

                if (dbStudent.Email == student.Email)
                {
                    await _studentRepository.Update(student);

                    return Ok(await _studentRepository.GetAll());
                }
                else
                {
                    var dbStudents = await _studentRepository.FindByCondition(s => s.Email.Equals(student.Email));
                    if (dbStudents.Count() == 0)
                    {
                        dbStudent.Email = student.Email;

                        await _studentRepository.Update(student);

                        return Ok(await _studentRepository.GetAll());
                    }
                    else
                    {
                        return NotFound("Sorry, Given email is already existing. :/");
                    }
                }
            }
            else
            {
                return NotFound("Sorry, but no student for given ID. :/");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
        {
            var dbStudent = await _studentRepository.GetById(id);
                
            if(dbStudent == null)
            {
                return NotFound("Sorry, but no student for you. :/");
            }
            else
            {
                await _studentRepository.Delete(id);
                return Ok(await _studentRepository.GetAll());

            }
        }
    }
}

