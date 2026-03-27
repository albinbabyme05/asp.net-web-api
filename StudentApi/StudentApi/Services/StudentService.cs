using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Dtos;
using StudentApi.Model;

namespace StudentApi.Services
{
    public class StudentService: IStudentService
    {
        private readonly AppDbContext _dbContext;
        public StudentService(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;  
        }

        //get  all student
        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            //1.fetch all student data from DB
            var student = await _dbContext.StudentsDB.ToListAsync();

            //2.send only required data to user
            var result = student.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                Email = s.Email,
                Course = s.Course,
                Department = s.Department,
            });

            //3.return data with status code
            return result;
        }

        // get student by id
        public async Task<StudentDto> GetStudentById(int id)
        {
            var student = await _dbContext.StudentsDB.FindAsync(id);

            if (student == null)
                return null;

            var result = new StudentDto
            {
                Name = student.Name,
                Age = student.Age,
                Email = student.Email,
                Course = student.Course,
                Department = student.Department,
            };

            return result;
        }


        //Add a sudent
        public async Task<StudentDto> CreateStudent(CreateStudentDto createStudent)
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
            var responseDto = new StudentDto
            {
                Name = newStudent.Name,
                Age = newStudent.Age,
                Email = newStudent.Email,
                Course = newStudent.Course,
                Department = newStudent.Department,
            };

            return responseDto;
        }

        // update the studnet
        public async Task<bool> UpdateStudent(int id, UpdateStudentDto updateStudent)
        {
            var student = await _dbContext.StudentsDB.FindAsync(id);

            if (student == null) return false;

            //check if user send the name/age/ other field=> update only required fields
            if (updateStudent.Name != null) student.Name = updateStudent.Name;

            if (updateStudent.Age.HasValue) student.Age = updateStudent.Age.Value;

            if (updateStudent.Email != null) student.Email = updateStudent.Email;

            if (updateStudent.Course != null)student.Course = updateStudent.Course;

            if (updateStudent.Department != null)student.Department = updateStudent.Department;

            await _dbContext.SaveChangesAsync();
            return true;

        }

        //delete a student by Id
        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _dbContext.StudentsDB.FindAsync(id);
            if (student == null) return false;

            _dbContext.StudentsDB.Remove(student);

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
