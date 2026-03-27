using System.ComponentModel.DataAnnotations;

namespace StudentApi.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [Range(5, 22)]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

       
        [StringLength(60)]
        public string? Department { get; set; }

        [StringLength(60)]
        public string? Course { get; set; }


    }
}
