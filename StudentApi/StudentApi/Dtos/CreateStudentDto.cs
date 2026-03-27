using System.ComponentModel.DataAnnotations;

namespace StudentApi.Dtos
{
    public class CreateStudentDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(1,100)]
        public int Age { get; set; }

        [Required][EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required][StringLength(60)]
        public string Department { get; set; } = string.Empty;

        [StringLength(60)]
        public string Course { get; set; } = string.Empty;

    }
}
