using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Configurations;
using StudentEnrollment.Data.Models;

namespace StudentEnrollment.Data.DbContext;

public class StudentEnrollmentDbContext(DbContextOptions<StudentEnrollmentDbContext> options)
    : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new CourseConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());

        builder.Entity<IdentityUser>(entity => entity.ToTable("Users", "identity"));
        builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRoles", "identity"));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims", "identity"));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins", "identity"));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims", "identity"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens", "identity"));
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
}