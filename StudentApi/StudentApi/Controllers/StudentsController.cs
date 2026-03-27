using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Dtos;
using StudentApi.Model;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext? _dbContext;

        public StudentsController(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        //get  all student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudent()
        {
            //1.fetch all student data from DB
            var student = await _dbContext.StudentsDB.ToListAsync();

            //2.send only required data to user
            var result = student.Select(s => new StudentDto
            {
                Name = s.Name,
                Age = s.Age,
                Email = s.Email,
                Course = s.Course,
                Department = s.Department,
            }).ToList();

            //3.return data with status code
            return Ok(result);
        }

        // get student by id
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            var student = await _dbContext.StudentsDB.FindAsync(id);

            if (student == null)
                return NotFound();

            var result = new StudentDto
            {
                Name = student.Name,
                Age = student.Age,
                Email = student.Email,
                Course = student.Course,
                Department = student.Department,
            };

            return Ok(result);
        }


        //Add a sudent
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateAStudent(CreateStudentDto createStudent)
        {
            // create a student object to add to List or database
            var newStudent = new Student
            {
                Name = createStudent.Name,
                Age = createStudent.Age,
                Email = createStudent.Email,
                Course = createStudent.Course,
                Department = createStudent.Department
            };

            //save new student in DB
            _dbContext.StudentsDB.Add(newStudent);
            await _dbContext.SaveChangesAsync();

            // provide minimal information to the client as response
            var newStudentDto = new StudentDto
            {
                Name = newStudent.Name,
                Age = newStudent.Age,
                Email = newStudent.Email,
                Course = newStudent.Course,
                Department = newStudent.Department,
            };

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudentDto);
        }

        // update the studnet
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentDto updateStudent, int id)
        {
            var student = await _dbContext.StudentsDB.FindAsync(id);

            if (student == null)
                return NotFound();

            //check if user send the name/age/ other field=> update only required fields
            if(updateStudent.Name != null)
                student.Name = updateStudent.Name;

            if (updateStudent.Age.HasValue)
                student.Age = updateStudent.Age.Value;

            if (updateStudent.Email != null)
                student.Email = updateStudent.Email;

            if (updateStudent.Course != null)
                student.Course = updateStudent.Course;

            if (updateStudent.Department != null)
                student.Department = updateStudent.Department;

            await _dbContext.SaveChangesAsync();

            return NoContent();


        }

        //delete a stduent byid
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _dbContext.StudentsDB.FindAsync(id);
            if (student == null)
                return NotFound();

            _dbContext.StudentsDB.Remove(student);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
