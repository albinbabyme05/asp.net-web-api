using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Data;
using StudentManagementApi.DTOs;
using StudentManagementApi.Model;

namespace StudentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDBContext _dbContext;
        public StudentController(StudentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetAllStudents()
        {
            var students = await _dbContext.studentsDB.ToListAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetByStudentId(int id)
        {
            var student = await _dbContext.studentsDB.FindAsync(id);

            if(student == null)
            {
                return NotFound($"Student with Id {id} was not found.");
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Students>> AddAstudent(CreateStudentDto studentdto)
        {
            var student = new Students
            {
                Name = studentdto.Name,
                Age = studentdto.Age,
                Department = studentdto.Department,
                Email = studentdto.Email,
            };
            _dbContext.studentsDB.Add(student);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByStudentId), new { id = student.Id }, student);
        }

    }
}
