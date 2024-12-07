using StudentEnrollment.DTOs.Course;
using StudentEnrollment.DTOs.Student;

namespace StudentEnrollment.DTOs.Enrollment;

public class EnrollmentDto
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    public CourseDto Course { get; set; }
    public StudentDto Student { get; set; }
}