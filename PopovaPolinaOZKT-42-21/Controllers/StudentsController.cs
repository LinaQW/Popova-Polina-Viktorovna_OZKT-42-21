using Microsoft.AspNetCore.Mvc;
using PopovaPolinaOZKT_42_21.Filters.StudentFilters;
using PopovaPolinaOZKT_42_21.Interfaces.StudentsInterfaces;
using Microsoft.EntityFrameworkCore;
using PopovaPolinaOZKT_42_21.DataBase;
using PopovaPolinaOZKT_42_21.DataBase.Models;
using System.Text.RegularExpressions;
using NuGet.DependencyResolver;

namespace PopovaPolinaOZKT_42_21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;
        public StudentDbContext _dbcontext;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService, StudentDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _dbcontext = context;
        }

        [HttpPost( "Фильтр")]
        public async Task<IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);
            return Ok(students);
        }

        /* [HttpGet("GetGroup ")]
         public IActionResult GetGroups()
         {
             var groups = _dbcontext.Groups.Select(g => new { g.GroupId, g.GroupName }).ToList();
             return Ok(groups);
         }

         [HttpGet("GetStudent")]
         public IActionResult GetStudents()
         {
             var students = _dbcontext.Students.Select(g => new { g.StudentId, g.FirstName, g.LastName, g.MiddleName }).ToList();
             return Ok(students);
         }*/


        [HttpPost("AddGroup")]
        public IActionResult CreateGroup([FromBody] DataBase.Models.Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbcontext.Groups.Add(group);
            _dbcontext.SaveChanges();
            return Ok(group);
        }




        [HttpPost("AddStudent")]
        public IActionResult CreateStudent([FromBody] StudentAddFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = _dbcontext.Groups.FirstOrDefault(g => g.GroupName == filter.GroupName);

            if (group == null)
            {
                return BadRequest("Группа не найдена.");
            }

            var student = new Student();
            student.FirstName = filter.Name;
            student.LastName = filter.Surname;
            student.MiddleName = filter.Patronym;
            student.GroupId = group.GroupId;
            _dbcontext.Students.Add(student);
            _dbcontext.SaveChanges();
            return Ok(student);
        }

        [HttpPut("EditGroup")]
        public IActionResult UpdateGroup(string groupname, [FromBody] StudentGroupFilter updatedGroup)
        {
            var existingGroup = _dbcontext.Groups.FirstOrDefault(g => g.GroupName == groupname);

            if (existingGroup == null)
            {
                return NotFound();
            }

            existingGroup.GroupName = updatedGroup.GroupName;
            _dbcontext.SaveChanges();

            return Ok();
        }


        [HttpPut("EdiyStudent")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentAddFilter filter)
        {
            var existingStudent = _dbcontext.Students.FirstOrDefault(g => g.StudentId == id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.FirstName = filter.Name;
            existingStudent.LastName = filter.Surname;
            existingStudent.MiddleName = filter.Patronym;
            existingStudent.GroupId = _dbcontext.Groups.FirstOrDefault(g => g.GroupName == filter.GroupName).GroupId;
            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpDelete("DeletStudent")]
        public IActionResult DeleteStudent(int id)
        {
            var existingStudent = _dbcontext.Students.FirstOrDefault(g => g.StudentId == id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            _dbcontext.Students.Remove(existingStudent);
            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpDelete("DeletGroup")]
        public IActionResult DeleteGroup(int id)
        {
            // Находим группу по заданному id
            var existingGroup = _dbcontext.Groups.FirstOrDefault(g => g.GroupId == id);
            if (existingGroup == null)
            {
                return NotFound("Группа не найдена.");
            }

            // Проверяем, есть ли связанные студенты
            var relatedStudents = _dbcontext.Students.Any(s => s.GroupId == id);
            if (relatedStudents)
            {
                return BadRequest("Нельзя удалить группу, так как она связана со студентами.");
            }

            // Если группа существует и нет связанных студентов, удаляем её
            _dbcontext.Groups.Remove(existingGroup);
            _dbcontext.SaveChanges();

            return Ok("Группа успешно удалена.");
        }


    }
}
