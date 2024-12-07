using StudentEnrollment.DTOs.Course;

namespace StudentEnrollment.DTOs.Student;

public class StudentDetailsDto : CreateStudentDto
{
    public List<CourseDto> Courses { get; set; } = [];
}