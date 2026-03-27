using System.ComponentModel.DataAnnotations;

namespace StudentApi.Dtos
{
    public class CreateStudentDto
    {
        [Required]
        public string? Name { get; set; }

        [Range(1,100)]
        public int Age { get; set; }

        [Required][EmailAddress]
        public string? Email { get; set; }

        [Required][StringLength(60)]
        public string? Department { get; set; }

        [StringLength(60)]
        public string? Course { get; set; }
        
    }
}
