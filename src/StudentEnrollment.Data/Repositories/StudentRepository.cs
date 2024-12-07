using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.DbContext;
using StudentEnrollment.Data.Models;

namespace StudentEnrollment.Data.Repositories;

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(StudentEnrollmentDbContext db) : base(db)
    {
    }

    public async Task<Student?> GetStudentDetails(int studentId)
    {
        var student = await Context.Students
            .Include(q => q.Enrollments).ThenInclude(q => q.Course)
            .FirstOrDefaultAsync(q => q.Id == studentId);

        return student;
    }
}