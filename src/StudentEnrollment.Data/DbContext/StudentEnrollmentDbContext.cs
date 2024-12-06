using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentEnrollment.Data.DbContext;

public class StudentEnrollmentDbContext(DbContextOptions<StudentEnrollmentDbContext> options)
    : IdentityDbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
}