using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Dtos;
using StudentApi.Model;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _iStudentService;

        public StudentsController(IStudentService iStudentService)
        {
            _iStudentService = iStudentService;
        }

        //get  all student
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<StudentDto>>> GetAllStudent(
            [FromQuery] int pageNumber=1,
            [FromQuery] int pageSize=5,
            [FromQuery] string? name=null,
            [FromQuery] string? department=null,
            [FromQuery] string? sortBy="id",
            [FromQuery] string? sortOrder="asc"
            )
        {
            // controll service pagnation and safety
            if(pageNumber<= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and Page size must be greater than 0.");
            }

            if (pageSize > 50)
            {
                pageNumber = 50;
            }

            //1.fetch all student data from DB with servicr class
            var students = await _iStudentService.GetAllStudents(pageNumber, pageSize,name, department, sortBy, sortOrder);
            return Ok(students);
        }

        // get student by id
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            var student = await _iStudentService.GetStudentById(id);

            if (student == null)return NotFound();
            return Ok(student);
        }


        //Add a sudent
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateAStudent(CreateStudentDto createStudent)
        {
            // create a student object to add to List or database
            var newStudent = await _iStudentService.CreateStudent(createStudent);
            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }

        // update the studnet
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentDto updateStudent, int id)
        {
            var isUpdated = await _iStudentService.UpdateStudent(id, updateStudent);

            if (!isUpdated) return NotFound();

            return NoContent();
        }

        //delete a stduent byid
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var isDeleted = await _iStudentService.DeleteStudent(id);

            if (!isDeleted) return NotFound();

            return NoContent();
        }

    }
}
