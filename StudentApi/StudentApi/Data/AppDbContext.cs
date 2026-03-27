
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StudentApi.Model;
using System;


namespace StudentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        //create table as field
        public DbSet<Student> StudentsDB { get; set; }
    }
}
