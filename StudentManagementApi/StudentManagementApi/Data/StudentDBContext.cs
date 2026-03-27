using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Model;

namespace StudentManagementApi.Data
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options)
        {
        }

        public DbSet<Students> studentsDB { get; set; }
    }
}
