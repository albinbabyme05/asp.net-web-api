using System.ComponentModel.DataAnnotations;

namespace StudentApi.Dtos
{
    public class CreateStudentDto
    {
        
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Course { get; set; }
        
    }
}
