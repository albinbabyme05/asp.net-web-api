using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.DTOs
{
    public class CreateStudentDto
    {
        [Required]
        public string Name { get; set; }

        [Range(18,60)]
        public int Age { get; set; }

        [Required]
        public string Department { get; set; }

        [EmailAddress]
        public string  Email { get; set; }

    }
}
