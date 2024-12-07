using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.DbContext;
using StudentEnrollment.Data.Models;

namespace StudentEnrollment.Data.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(StudentEnrollmentDbContext db) : base(db)
    {
    }

    public async Task<Course?> GetStudentList(int courseId)
    {
        var course = await Context.Courses
            .Include(q => q.Enrollments).ThenInclude(q => q.Student)
            .FirstOrDefaultAsync(q => q.Id == courseId);

        return course;
    }
}