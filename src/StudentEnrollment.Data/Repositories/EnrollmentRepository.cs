using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.DbContext;
using StudentEnrollment.Data.Models;

namespace StudentEnrollment.Data.Repositories;

public class EnrollmentRepository(StudentEnrollmentDbContext db)
    : GenericRepository<Enrollment>(db), IEnrollmentRepository;