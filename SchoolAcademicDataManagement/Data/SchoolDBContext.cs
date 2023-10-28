using Microsoft.EntityFrameworkCore;
using SchoolAcademicDataManagement.Models.Academic;
using SchoolAcademicDataManagement.Models.User;

namespace SchoolAcademicDataManagement.Data
{
    public class SchoolDBContext : DbContext
    {
        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}

