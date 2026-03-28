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

        // adding logging
        private readonly ILogger<StudentService> _iLogger;
        
        public StudentService(AppDbContext dbcontext, ILogger<StudentService> iLogger)
        {
            _dbContext = dbcontext;
            _iLogger = iLogger;
        }

        //get  all student
        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            _iLogger.LogInformation("Fetching all students from database.");
                                                  
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
            }).ToList();

            _iLogger.LogInformation($"Fetched {result.Count} students successfully.");

            //3.return data with status code
            return result;
        }

        // get student by id
        public async Task<StudentDto> GetStudentById(int id)
        {
            _iLogger.LogInformation("Fetching student with id {StudentId}.", id);

            var student = await _dbContext.StudentsDB.FindAsync(id);

            if (student == null)
            {
                _iLogger.LogInformation("Student with id {StudentId} was not found.", id);
                return null;
            }
                

            var result = new StudentDto
            {
                Name = student.Name,
                Age = student.Age,
                Email = student.Email,
                Course = student.Course,
                Department = student.Department,
            };

            _iLogger.LogInformation("Fetched student with id {StudentId}.", id);
            return result;
        }


        //Add a sudent
        public async Task<StudentDto> CreateStudent(CreateStudentDto createStudent)
        {
            _iLogger.LogInformation("Creating a new student with email {Email}.", createStudent.Email);

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

            _iLogger.LogInformation("Created a new student with email {Email}.", createStudent.Email);

            return responseDto;
        }

        // update the studnet
        public async Task<bool> UpdateStudent(int id, UpdateStudentDto updateStudent)
        {
            _iLogger.LogInformation("Updating student with id {StudentId}.", id);

            var student = await _dbContext.StudentsDB.FindAsync(id);

            if (student == null)
            {
                _iLogger.LogWarning("Update failed. Student with id {StudentId} was not found.", id);
                return false;
            }

            //check if user send the name/age/ other field=> update only required fields
            if (updateStudent.Name != null) student.Name = updateStudent.Name;

            if (updateStudent.Age.HasValue) student.Age = updateStudent.Age.Value;

            if (updateStudent.Email != null) student.Email = updateStudent.Email;

            if (updateStudent.Course != null)student.Course = updateStudent.Course;

            if (updateStudent.Department != null)student.Department = updateStudent.Department;

            await _dbContext.SaveChangesAsync();

            _iLogger.LogInformation("Student with id {StudentId} updated successfully.", id);

            return true;

        }

        //delete a student by Id
        public async Task<bool> DeleteStudent(int id)
        {
            _iLogger.LogInformation("Deleting student with id {StudentId}.", id);

            var student = await _dbContext.StudentsDB.FindAsync(id);
            if (student == null)
            {
                _iLogger.LogWarning("Delete failed. Student with id {StudentId} was not found.", id);
                return false;
            }

            _dbContext.StudentsDB.Remove(student);

            await _dbContext.SaveChangesAsync();

            _iLogger.LogInformation("Student with id {StudentId} deleted successfully.", id);

            return true;
        }
    }
}
