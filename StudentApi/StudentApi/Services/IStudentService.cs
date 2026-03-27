using StudentApi.Dtos;


namespace StudentApi.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudents();
        Task<StudentDto> CreateStudent(CreateStudentDto createStudent);
        Task<StudentDto> GetStudentById(int id);
        Task<bool> UpdateStudent(int id, UpdateStudentDto updateStudent);
        Task<bool> DeleteStudent(int id);

    }
}
